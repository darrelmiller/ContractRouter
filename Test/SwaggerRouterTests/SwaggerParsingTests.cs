using Newtonsoft.Json.Linq;
using SwaggerRouter;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace SwaggerRouterTests
{
    public static class JObjectExtensions
    {
        public static MemoryStream ToMemoryStream(this JObject jObject)
        {
            var stream = new MemoryStream();

            var sw = new StreamWriter(stream);
            sw.Write(jObject.ToString());
            sw.Flush();

            stream.Position = 0;
            return stream;
        }
        public static MemoryStream ToMemoryStream(this string somestring)
        {
            var stream = new MemoryStream();

            var sw = new StreamWriter(stream);
            sw.Write(somestring);
            sw.Flush();

            stream.Position = 0;
            return stream;
        }
    }

    public class SwaggerParsingTests
    {
        private ITestOutputHelper _Output;
        public SwaggerParsingTests(ITestOutputHelper output)
        {
            _Output = output;
        }

        [Fact]
        public void ParseSimpleSwagger()
        {

            var jObject = new JObject(new JProperty("swagger", "2.0"),
                                        new JProperty("x-unknown", "blah"),
                                        new JProperty("info",
                                                new JObject(
                                                        new JProperty("title", "This is the title"),
                                                        new JProperty("description", "This is the description"),
                                                        new JProperty("version", "1.1")
                                                        ))
                                      );


            JsonStreamingParser.ParseStream(jObject.ToMemoryStream(), new ConsoleSwaggerJsonConsumer(_Output));

        }


        [Fact]
        public void ParseSwaggerPaths()
        {
            var swaggerDoc = new SwaggerDocument();
            swaggerDoc.AddPath("foo", p => {
                p.AddOperation("get", "fooget");
            });

            swaggerDoc.AddPath("bar", p => {
                p.AddOperation("get", "barget");
            });

            swaggerDoc.AddPath("baz", p => {
                p.AddOperation("get", "bazget");
                p.AddOperation("put", "bazput");
            });


            var swaggerstring = swaggerDoc.ToString();

            _Output.WriteLine(swaggerstring);

            JsonStreamingParser.ParseStream(swaggerstring.ToMemoryStream(), new ConsoleSwaggerJsonConsumer(_Output));

        }


        [Fact]
        public void ParseEmbeddedSwagger()
        {

           var stream = typeof(SwaggerParsingTests).Assembly.GetManifestResourceStream("SwaggerRouterTests.forecast.io.swagger.json");

            JsonStreamingParser.ParseStream(stream, new ConsoleSwaggerJsonConsumer(_Output));

        }


    }
}