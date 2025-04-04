namespace DataSystem.Shared.Helpers.Environments
{
    public static class EnvironmentVariablesHelper
    {
        public static EnvironmentVariablesDTO GetEnvironmentVariable()
        {
            return new EnvironmentVariablesDTO
            {
                DatabaseLogLevel = Environment.GetEnvironmentVariable("DatabaseLogLevel")
                    ?? throw new InvalidOperationException("DatabaseLogLevel environment variable is not set"),
            };
        }
    }
}
