// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elaia.Auth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
              var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    // .AddFilter("Microsoft", LogLevel.Warning)
                    // .AddFilter("System", LogLevel.Warning)
                    // .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    //.AddDebug()
                    .AddConsole();
                    // .AddEventLog();
            });

            var host = CreateHostBuilder(args).Build();
            
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting application");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
                //     .ConfigureServices(serviceCollection => serviceCollection
                //         .Configure<AzureFileLoggerOptions>(options =>
                //         {
                //             options.FileName = "azure-diagnostics-";
                //             options.FileSizeLimit = 5 * 1024;
                //             options.RetainedFileCountLimit = 10;
                //         })
                //         .Configure<AzureBlobLoggerOptions>(options =>
                //         {
                //             options.BlobName = "log.txt";
                //         })
                //     )
                // .ConfigureLogging(logging =>
                // {
                //     // logging.ClearProviders();
                //     // logging.AddConsole();
                // })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
