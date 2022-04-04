// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using MTConnect.Agents;
using MTConnect.Agents.Configuration;
using MTConnect.Applications.Loggers;
using NLog.Web;
using System;

namespace MTConnect.Applications
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                // Set WebApplication Options
                var options = new WebApplicationOptions
                {
                    Args = args,
                    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
                };

                // Create WebApplication Builder
                var builder = WebApplication.CreateBuilder(options);
                ConfigureBuilder(builder);
                AddServices(builder);

                // Create WebApplication
                var app = builder.Build();
                ConfigureServices(app);

                // Run WebApplication
                app.Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        // This is the method that should be able to be used instead of CreateHostBuilder()
        private static void ConfigureBuilder(WebApplicationBuilder builder)
        {
            // Set to allow Windows Service
            builder.Host.UseWindowsService();

            // Add Logging
            builder.Host.UseNLog();
            builder.Logging.AddConsole();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            var configuration = MTConnectAgentConfiguration.Read();
            if (configuration != null)
            {
                // Create MTConnectAgent
                var agent = new MTConnectAgent(configuration);
                agent.Version = new Version(1, 8);
                builder.Services.AddSingleton<IMTConnectAgent>(agent);

                // Individual Logger Classes
                builder.Services.AddSingleton<AgentLogger>();
                builder.Services.AddSingleton<AgentValidationLogger>();
                builder.Services.AddSingleton<AdapterLogger>();
                builder.Services.AddSingleton<AdapterShdrLogger>();

                // Add the AgentService that handles the MTConnect Agent
                builder.Services.AddHostedService<AgentService>();

                builder.WebHost.UseKestrel(o =>
                    {
                        o.ListenAnyIP(configuration.Port);
                    });
            }

            // Add Controllers
            builder.Services.AddControllers();
        }

        private static void ConfigureServices(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}