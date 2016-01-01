﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using OpenApiRouter;
namespace SampleApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var stream = typeof(WebApiConfig).Assembly
               .GetManifestResourceStream("SampleApi.openapi.json");

            config.Routes.Add("default", new OpenApiRouter.OpenApiRouter(stream));
            
        }
    }
}
