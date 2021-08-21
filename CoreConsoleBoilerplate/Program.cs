using Microsoft.Extensions.Configuration;

using Serilog;

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CoreConsoleBoilerplate
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Create the builder
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            // Setup Serilog's logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Application starting...");

            // Setup host
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IApp, App>();
                })
                .UseSerilog()
                .Build();

            // Create the service
            var app = ActivatorUtilities.CreateInstance<App>(host.Services);

            // Run
            app.Run();
        }

        /// <summary>
        ///     Sets up the <see cref="IConfigurationBuilder"/>
        /// </summary>
        /// <param name="builder"></param>
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production"}.json",
                    optional: true)
                .AddEnvironmentVariables();
        }
    }
}
