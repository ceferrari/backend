using System;
using AWS.Logger;
using Backend.Shared.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace Backend.Web
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new AWSLoggerConfig
            {
                Credentials = Backend.Api.Program.CredentialsCarlos,
                Region = "us-east-1",
                LogGroup = "hackaiti",
                DisableLogGroupCreation = true
            };

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Verbose()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.Enrich.FromLogContext()
                .Enrich.With(new LogsEnricher())
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithIgnoreStackTraceAndTargetSiteExceptionFilter()
                    .WithDefaultDestructurers()
                    .WithRootName("data"))
#if DEBUG
                .WriteTo.Console(new LogsJsonFormatter())
                //.WriteTo.AWSSeriLog(configuration, null, new LogsJsonFormatter())
#else
                .WriteTo.AWSSeriLog(configuration, null, new LogsJsonFormatter())
#endif
                .CreateLogger();

            try
            {
                Log.Information("starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "web host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
