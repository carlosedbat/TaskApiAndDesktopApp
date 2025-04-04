using DotNetEnv;

namespace DataSystem.FrontendWpf.Helpers.Environment
{
    public static class EnvironmentHelper
    {

        public static EnvironmentFrontendVariablesDTO Variables = new EnvironmentFrontendVariablesDTO();

        public static void GetVariablesFromDotEnv()
        {
            Env.Load();
            SetBackendApi();
        }

        private static void SetBackendApi()
        {
            string? backendApi = System.Environment.GetEnvironmentVariable("BACKEND_API");

            Variables.BackendApi = !string.IsNullOrEmpty(backendApi) ? backendApi : "https://127.0.0.1:7269/api";
        }
    }
}
