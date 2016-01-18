using Newtonsoft.Json.Linq;
using ContractRouter;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Hapikit;

namespace ContractRouterTests
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

    public class ParsingTests
    {
        private ITestOutputHelper _Output;
        public ParsingTests(ITestOutputHelper output)
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

            var doc = new OpenApiDocument();
            JsonStreamingParser.ParseStream(jObject.ToMemoryStream(), doc,OpenApiVocab.Create());

        }


        [Fact]
        public void ParseSwaggerPaths()
        {
            var swaggerDoc = new OpenApiDocument();
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

            var newDoc = new OpenApiDocument();
            JsonStreamingParser.ParseStream(swaggerstring.ToMemoryStream(), newDoc,OpenApiVocab.Create());

        }


        [Fact]
        public void ParseEmbeddedSwagger()
        {

           var stream = typeof(ParsingTests).Assembly.GetManifestResourceStream(typeof(ParsingTests),"forecast.io.swagger.json");

            JsonStreamingParser.ParseStream(stream, new OpenApiDocument(),OpenApiVocab.Create());

        }


    }
}