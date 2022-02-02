using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EvidenceKnih
{
    /// <summary>
    /// Creates and configures a web host
    /// </summary>
    public class Program
    {
        // Begins execution
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Creates web host
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}