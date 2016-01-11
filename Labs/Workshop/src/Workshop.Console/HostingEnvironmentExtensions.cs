using Microsoft.AspNet.Hosting;

namespace Workshop.Console
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsTesting(this IHostingEnvironment env)
        {
            return env.EnvironmentName == "Testing";
        } 
    }
}