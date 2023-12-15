#!/bin/bash

call_fixed_endpoint() {
    d=$(date +"%H:%M:%S:%N")
    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/fixed/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed/$1
    echo ""
}

call_fixed_endpoints() {
    d=$(date +"%H:%M:%S:%N")

    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/fixed/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed/$1
    echo ""

    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/fixed2/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed2/$1
    echo ""
}

call_sliding_endpoint() {
     d=$(date +"%H:%M:%S")
    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/sliding/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/sliding/$1
    echo ""
}

call_token_endpoint() {
     d=$(date +"%H:%M:%S:%N")
    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/token/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/token/$1
    echo ""
}

call_concurrency_endpoint() {
     d=$(date +"%H:%M:%S:%N")
    curl -s -o /dev/null -w "$d : http://localhost:5051/api/v1/rate-limit/concurrency/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/concurrency/$1
    echo ""
}


test_fixed_window_limiter() {
    echo "Assuming:"
    echo "RATE_LIMITING_PERMIT_LIMIT set to 5."
    echo "RATE_LIMITING_WINDOW set to 10."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0."

    echo ""
    echo "making 10 calls in parallel. 5 should pass and 5 should fail"
    seq 10 | parallel -j 10 call_fixed_endpoint {%}

    echo ""
    echo "waiting 5 seconds and all 10 should still fail"
    sleep 5
    seq 10 | parallel -j 10 call_fixed_endpoint {%}

    echo ""
    echo "waiting 6 seconds and 5 should pass as the 10s window has passed"
    sleep 5
    seq 10 | parallel -j 10 call_fixed_endpoint {%}
}

test_fixed_window_limiter_across_two_endpoints() {
    echo "Assuming:"
    echo "RATE_LIMITING_PERMIT_LIMIT set to 5."
    echo "RATE_LIMITING_WINDOW set to 10."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0."

    echo ""
    echo "making 10 calls in parallel. 5 should pass and 5 should fail"
    seq 5 | parallel -j 5 call_fixed_endpoints {%}

    echo ""
    echo "waiting 5 seconds and all 10 should still fail"
    sleep 5
    seq 5 | parallel -j 5 call_fixed_endpoints {%}

    echo ""
    echo "waiting 6 seconds and 5 should pass as the 10s window has passed"
    sleep 5
    seq 5 | parallel -j 5 call_fixed_endpoints {%}
}

test_sliding_window_limiter() {
    echo "Assuming:"
    echo "RATE_LIMITING_PERMIT_LIMIT set to 5."
    echo "RATE_LIMITING_WINDOW set to 10."
    echo "RATE_LIMITING_SEGMENTS_PER_WINDOW set to 5."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0"

    echo ""
    echo "making 5 calls will fill up the last segment in the window"
    seq 5 | parallel -j 5 call_sliding_endpoint {%}

    echo ""
    echo "most of these calls should fail"
    for i in $(seq 1 10);
    do
        call_sliding_endpoint $i
        sleep 1
    done

    echo ""
    echo "the filled segment will fall away and these calls can pass again"
    seq 5 | parallel -j 5 call_sliding_endpoint {%}
}

test_sliding_window_limiter2() {
    echo "Assuming:"
    echo "RATE_LIMITING_PERMIT_LIMIT set to 5."
    echo "RATE_LIMITING_WINDOW set to 10."
    echo "RATE_LIMITING_SEGMENTS_PER_WINDOW set to 5."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0"

    echo ""
    echo "making 2 calls per segment should space the calls across all segments"
    for i in $(seq 1 10);
    do
        call_sliding_endpoint $i
        sleep 1
    done

    echo "increase to 4 calls per segment to show segments free up"
    for i in $(seq 1 10);
    do
        call_sliding_endpoint $i
        sleep 0.5
    done

}

test_token_bucket_limiter() {
    echo "Assuming:"
    echo "RATE_LIMITING_TOKEN_LIMIT set to 5."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0."
    echo "RATE_LIMITING_REPLENISHMENT_PERIOD set to 5."
    echo "RATE_LIMITING_TOKENS_PER_PERIOD set to 2."
    echo "RATE_LIMITING_AUTO_REPLENISHMENT set to True."

    echo ""
    echo "bucket 1 should have 5 tokens"
    echo "bucket 2 onwards should get 2 tokens refreshed"
    for i in $(seq 1 4);
    do
        echo "bucket $i"
        seq 5 | parallel -j 5 call_token_endpoint {%}
        sleep 5
    done
}

test_concurrencyt_limiter() {
    echo "Assuming:"
    echo "RATE_LIMITING_PERMIT_LIMIT set to 5."
    echo "RATE_LIMITING_QUEUE_PROCESSING_ORDER set to OldestFirst."
    echo "RATE_LIMITING_QUEUE_LIMIT set to 0."

    echo ""
    echo "Use up the token limit"
    echo "each call should take 0.5s to complete"
    seq 5 | parallel -j 5 call_concurrency_endpoint 0
    sleep 0.5

    echo ""
    echo "Every 0.5s, we should be able to accept 5 more calls since the prevuious 5 have completed"
    for i in $(seq 1 5);
    do
        echo ""
        echo "5 pass 1 fail"
        seq 6 | parallel -j 6 call_concurrency_endpoint {%}
        sleep 0.5
    done
}

export -f call_fixed_endpoint
export -f call_fixed_endpoints
export -f call_sliding_endpoint
export -f call_token_endpoint
export -f call_concurrency_endpoint
export -f test_fixed_window_limiter
export -f test_fixed_window_limiter_across_two_endpoints
export -f test_sliding_window_limiter
export -f test_sliding_window_limiter2
export -f test_token_bucket_limiter
export -f test_concurrencyt_limiter



# test_fixed_window_limiter
# test_fixed_window_limiter_across_two_endpoints
# test_sliding_window_limiter
# test_sliding_window_limiter2
# test_token_bucket_limiter
test_concurrencyt_limiter