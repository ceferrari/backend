using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Shared.Utilities
{
    public static class Retry
    {
        private const int RetryIntervalSeconds = 10;
        private const int MxAttemptCount = 10;

        public static void Do(
            Action action,
            int retryInterval = RetryIntervalSeconds,
            int maxAttemptCount = MxAttemptCount)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        public static T Do<T>(
            Func<T> action,
            int retryInterval = RetryIntervalSeconds,
            int maxAttemptCount = MxAttemptCount)
        {
            var exceptions = new List<Exception>();

            for (var attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(retryInterval));
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }

        public static async Task DoAsync(
            Func<Task> action,
            int retryInterval = RetryIntervalSeconds,
            int maxAttemptCount = MxAttemptCount)
        {
            await DoAsync<Task<object>>(async () =>
            {
                await action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        public static async Task<T> DoAsync<T>(
            Func<Task<T>> action,
            int retryInterval = RetryIntervalSeconds,
            int maxAttemptCount = MxAttemptCount)
        {
            var exceptions = new List<Exception>();

            for (var attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(retryInterval));
                    }
                    return await action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
