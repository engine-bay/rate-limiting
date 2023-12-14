namespace EngineBay.RateLimiting.Tests
{
    using System;
    using System.Globalization;
    using System.Threading.RateLimiting;
    using EngineBay.RateLimiting;
    using Xunit;

    public class RateLimitingConfigurationTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AutoReplenishmentReturnsEnvVar(bool autoReplenishment)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.AUTOREPLENISHMENT, autoReplenishment.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(autoReplenishment, RateLimitingConfiguration.AutoReplenishment());
        }

        [Fact]
        public void AutoReplenishmentReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.AUTOREPLENISHMENT, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultAutoReplenishment, RateLimitingConfiguration.AutoReplenishment());
        }

        [Theory]
        [InlineData(11)]
        [InlineData(101)]
        public void PermitLimitReturnsEnvVar(int permitLimit)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.PERMITLIMIT, permitLimit.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(permitLimit, RateLimitingConfiguration.PermitLimit());
        }

        [Fact]
        public void PermitLimitReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.PERMITLIMIT, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultPermitLimit, RateLimitingConfiguration.PermitLimit());
        }

        [Theory]
        [InlineData(12)]
        [InlineData(102)]
        public void QueueLimitReturnsEnvVar(int queueLimit)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.QUEUELIMIT, queueLimit.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(queueLimit, RateLimitingConfiguration.QueueLimit());
        }

        [Fact]
        public void QueueLimitReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.QUEUELIMIT, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultQueueLimit, RateLimitingConfiguration.QueueLimit());
        }

        [Theory]
        [InlineData(QueueProcessingOrder.OldestFirst)]
        [InlineData(QueueProcessingOrder.NewestFirst)]
        public void QueueProcessingOrderReturnsEnvVar(QueueProcessingOrder queueProcessingOrder)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.QUEUEPROCESSINGORDER, queueProcessingOrder.ToString());
            Assert.Equal(queueProcessingOrder, RateLimitingConfiguration.QueueProcessingOrder());
        }

        [Fact]
        public void QueueProcessingOrderReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.QUEUEPROCESSINGORDER, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultQueueProcessingOrder, RateLimitingConfiguration.QueueProcessingOrder());
        }

        [Theory]
        [InlineData(13)]
        [InlineData(103)]
        public void ReplenishmentPeriodReturnsEnvVar(int replenishmentPeriod)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.REPLENISHMENTPERIOD, replenishmentPeriod.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(replenishmentPeriod, RateLimitingConfiguration.ReplenishmentPeriod());
        }

        [Fact]
        public void ReplenishmentPeriodReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.REPLENISHMENTPERIOD, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultReplenishmentPeriod, RateLimitingConfiguration.ReplenishmentPeriod());
        }

        [Theory]
        [InlineData(14)]
        [InlineData(104)]
        public void SegmentsPerWindowReturnsEnvVar(int segmentsPerWindow)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.SEGMENTSPERWINDOW, segmentsPerWindow.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(segmentsPerWindow, RateLimitingConfiguration.SegmentsPerWindow());
        }

        [Fact]
        public void SegmentsPerWindowReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.SEGMENTSPERWINDOW, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultSegmentsPerWindow, RateLimitingConfiguration.SegmentsPerWindow());
        }

        [Theory]
        [InlineData(15)]
        [InlineData(105)]
        public void TokenLimitReturnsEnvVar(int tokenLimit)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.TOKENLIMIT, tokenLimit.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(tokenLimit, RateLimitingConfiguration.TokenLimit());
        }

        [Fact]
        public void TokenLimitReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.TOKENLIMIT, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultTokenLimit, RateLimitingConfiguration.TokenLimit());
        }

        [Theory]
        [InlineData(16)]
        [InlineData(106)]
        public void TokensPerPeriodReturnsEnvVar(int tokensPerPeriod)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.TOKENSPERPERIOD, tokensPerPeriod.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(tokensPerPeriod, RateLimitingConfiguration.TokensPerPeriod());
        }

        [Fact]
        public void TokensPerPeriodReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.TOKENSPERPERIOD, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultTokensPerPeriod, RateLimitingConfiguration.TokensPerPeriod());
        }

        [Theory]
        [InlineData(17)]
        [InlineData(107)]
        public void WindowReturnsEnvVar(int window)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.WINDOW, window.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(window, RateLimitingConfiguration.Window());
        }

        [Fact]
        public void WindowReturnsDefault()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableConstants.WINDOW, string.Empty);
            Assert.Equal(DefaultRateLimitingConfigurationConstants.DefaultWindow, RateLimitingConfiguration.Window());
        }
    }
}