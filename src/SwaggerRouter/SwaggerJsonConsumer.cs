using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerRouter
{
        public class SwaggerJsonConsumer : IJsonConsumer
        {
            private Dictionary<string, ParseMap> _ParseMaps;

            public SwaggerJsonConsumer()
            {
                _ParseMaps = new Dictionary<string, ParseMap>();

                _ParseMaps[""] = new ParseMap(
                    new Dictionary<string, Action<object, object>> {
                                    //{"swagger",(p, t) => output.WriteLine($"Swagger Version {p}")},
                                    //{"host",(p,t) => output.WriteLine($"Host : {p}")},
                                    //{"basePath",(p,t) => output.WriteLine($"Base path :  {p}")},
                                    //{"schemes",(p,t) => output.WriteLine($"Schemes :  {p}")}

                                        });
                _ParseMaps["paths"] = new ParseMap(null)
                {
                    DefaultParse = (k, p, t) => { /* NOP */ }
                };

                _ParseMaps["ops"] = new ParseMap(null)
                {
                    DefaultParse = (k, p, t) => { /* NOP */ }
                };


            //_ParseMaps["info"] = new ParseMap(
            //    new Dictionary<string, Action<object, object>> {
            //                    {"title",(p,t) => output.WriteLine($"Title : {p}")},
            //                    {"description",(p,t) => output.WriteLine($"{p}")},
            //                    {"version",(p,t) => output.WriteLine($"{p}")},
            //                        });
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
                }
                else if (_ParseMaps.TryGetValue(currentContext.PropertyName, out parsemap))
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
                object newObject = null;
                
                switch (objectName) {
                    case "paths":
                        break;
                default:
                    // use currentContext to know what kind of object to create
                    switch(currentContext.PropertyName)
                    {
                        case "paths":
                            break;
                        case "???":
                            break;
                    }
                    break;
                };

                return newObject;
            }
        }
    }

