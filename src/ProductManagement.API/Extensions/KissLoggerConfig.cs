﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;

namespace ProductManagement.API.Extensions
{
    public static class KissLoggerConfig
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options => {
                ConfigureKissLog(options, configuration);
            }); 

            return app;
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration)
        {
            options.Listeners.Add(new RequestLogsApiListener(
                new Application(
                configuration["KissLog.OrganizationId"],
                configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
        }
    }
}
