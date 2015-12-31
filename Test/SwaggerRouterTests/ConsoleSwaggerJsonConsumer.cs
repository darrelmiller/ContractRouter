using SwaggerRouter;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace SwaggerRouterTests
{
    public class ConsoleSwaggerJsonConsumer : IJsonConsumer
    {
        private Dictionary<string, ParseMap> _ParseMaps;
        private ITestOutputHelper _Output;
        public ConsoleSwaggerJsonConsumer(ITestOutputHelper output)
        {
            _Output = output;
            _ParseMaps = new Dictionary<string, ParseMap>();

            _ParseMaps[""] = new ParseMap(
                new Dictionary<string, Action<object, object>> {
                                    {"swagger",(p, t) => output.WriteLine($"Swagger Version {p}")},
                                    {"host",(p,t) => output.WriteLine($"Host : {p}")},
                                    {"basePath",(p,t) => output.WriteLine($"Base path :  {p}")},
                                    {"schemes",(p,t) => output.WriteLine($"Schemes :  {p}")}

                                    });
            _ParseMaps["paths"] = new ParseMap(null)
            {
                DefaultParse = (k,p, t) => { output.WriteLine($"path : {k}"); }
            };
               


            _ParseMaps["info"] = new ParseMap(
                new Dictionary<string, Action<object, object>> {
                                    {"title",(p,t) => output.WriteLine($"Title : {p}")},
                                    {"description",(p,t) => output.WriteLine($"{p}")},
                                    {"version",(p,t) => output.WriteLine($"{p}")},
                                    });
        }
        public ParseMap GetParseMap(ParseContextObject currentContext, string objectName)
        {
            ParseMap parsemap = null;

            if (_ParseMaps.TryGetValue(objectName, out parsemap))
            {
                return parsemap;
                // TODO: I don't think currentContext.PropertyName is the right key here
                // because the context object of an operation is a path which doesn't have a constrainted name
                // I think ContextObject needs to have some kind of type identifier.
                // Not sure if typeof(ContextObject) is the right thing.
            } else if (_ParseMaps.TryGetValue(currentContext.PropertyName, out parsemap))
            {
                return parsemap;
            }
            else
            {
                return new ParseMap(null);
            };
        }

        public object CreateObject(ParseContextObject currentContext, string objectName)
        {
            
            _Output.WriteLine($"Creating object {objectName}");
            return null;
        }
    }
}