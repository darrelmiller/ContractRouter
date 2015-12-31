using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SampleApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var stream = typeof(WebApiConfig).Assembly
               .GetManifestResourceStream("SampleApi.swagger.json");

            // read swagger stream
            // Web API routes

            config.Routes.Add("SwaggerRouter", new SwaggerRouter.SwaggerRouter(stream));
            
        }
    }
}
