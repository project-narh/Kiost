using System;
using System.Net;
using System.Threading.Tasks;
using Server.Web;
using Server.Database;
using dotenv.net;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DotEnv.Load();

            Console.WriteLine("=== 레스토랑 API 서버 시작 ===");

            var webServer = CreateHostBuilder(args).Build();
            await webServer.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseKestrel(options =>
                    {
                        options.ListenAnyIP(8259);

                        // HTTPS 설정 (인증서가 있는 경우)
                        var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
                        var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");

                        if (!string.IsNullOrEmpty(certPath) && !string.IsNullOrEmpty(certPassword))
                        {
                            options.ListenAnyIP(443, listenOptions =>
                            {
                                listenOptions.UseHttps(certPath, certPassword);
                            });
                        }
                    });
                });
    }
}