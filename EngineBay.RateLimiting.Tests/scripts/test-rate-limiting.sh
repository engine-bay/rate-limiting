call_fixed_endpoint() {
    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/fixed/$1  : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed/$1
    echo ""
}

call_fixed_endpoints() {
    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/fixed/$1  : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed/$1
    echo ""

    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/fixed2/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/fixed2/$1
    echo ""
}

call_sliding_endpoint() {
    date +"%H:%M:%S"
    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/sliding/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/sliding/$1
    echo ""
}

call_token_endpoint() {
    date +"%H:%M:%S"
    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/token/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/token/$1
    echo ""
}

call_concurrency_endpoint() {
    date +"%H:%M:%S"
    curl -s -o /dev/null -w "http://localhost:5051/api/v1/rate-limit/concurrency/$1 : %{http_code}" http://localhost:5051/api/v1/rate-limit/concurrency/$1
    echo ""
}


test_fixed_window_limiter() {
    seq 10 | parallel -j 10 call_fixed_endpoint {%}
}

test_fixed_window_limiter_across_two_endpoints() {
    seq 5 | parallel -j 5 call_fixed_endpoints {%}
}

test_sliding_window_limiter_across_two_endpoints() {
     seq 1 | parallel -j 1 call_sliding_endpoint {%}
}

test_token_bucket_limiter() {
    seq 5 | parallel -j 5 call_token_endpoint 1
    sleep 1
    seq 2 | parallel -j 2 call_token_endpoint 2
    sleep 1
    seq 2 | parallel -j 2 call_token_endpoint 3
    sleep 1
    seq 2 | parallel -j 2 call_token_endpoint 4
    sleep 1
    seq 2 | parallel -j 2 call_token_endpoint 5
}

test_concurrencyt_limiter() {
    seq 5 | parallel -j 5 call_concurrency_endpoint 1
    sleep 0.5
    seq 6 | parallel -j 6 call_concurrency_endpoint 2
    sleep 0.5
    seq 6 | parallel -j 6 call_concurrency_endpoint 3
    sleep 0.5
    seq 6 | parallel -j 6 call_concurrency_endpoint 4
}

export -f call_fixed_endpoint
export -f call_fixed_endpoints
export -f call_sliding_endpoint
export -f call_token_endpoint
export -f call_concurrency_endpoint
export -f test_fixed_window_limiter
export -f test_fixed_window_limiter_across_two_endpoint
export -f test_sliding_window_limiter_across_two_endpoints
export -f test_token_bucket_limiter
export -f test_concurrencyt_limiter


#test_fixed_window_limiter
#test_fixed_window_limiter_across_two_endpoint
#test_sliding_window_limiter_across_two_endpoints
#test_token_bucket_limiter
test_concurrencyt_limiter