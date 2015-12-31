using Newtonsoft.Json.Linq;
using SwaggerRouter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SwaggerRouterTests
{
    public class ConsoleLogger : IObserver<KeyValuePair<string,object>>
    {
        readonly ITestOutputHelper output;

        public ConsoleLogger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            
            this.output.WriteLine($"{value.Key} {value.Value}");
            
        }
    }

    public class RebootParseTests
    {
        readonly ITestOutputHelper output;

        public RebootParseTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ParseTerm()
        {
            var x = new Vocabulary();
            x.AddTermHandler<LocalType,string>("foo", (s, o) => {
                s.Foo = o;
                });
            x.AddTermHandler<LocalType, int>("baz", (s, o) => {
                s.Baz = o;
            });

            var doc = new JObject(new JProperty("foo", "bar"), new JProperty("baz",22));

            var parser = new JsonStreamingParser();

            var localObject = new LocalType();
            JsonStreamingParser.ParseStream(doc.ToMemoryStream(), localObject  , x);

            Assert.Equal("bar", localObject.Foo);
            Assert.Equal(22, localObject.Baz);
        }

        [Fact]
        public void ParseTermInChild()
        {
            var x = new Vocabulary();

            x.AddTermHandler("child", (s, o) => {
                var subject = (LocalType)s.Subject;
                subject.Child = new LocalChildType();
                return new Context { Subject = subject.Child };
            });

            x.AddTermHandler<LocalChildType, string>("Ick", (s, o) => {
                s.Ick = o;
            });

            x.AddTermHandler<LocalChildType, string>("Ock", (s, o) => {
                s.Ock = o;
            });

            var doc = new JObject(new JProperty("foo", "bar"), 
                new JProperty("child", new JObject( new JProperty("Ick","much"), new JProperty("Ock", "wow"))));

            var parser = new JsonStreamingParser();

            var localObject = new LocalType();
            JsonStreamingParser.ParseStream(doc.ToMemoryStream(), localObject , x);

            Assert.Equal("much", localObject.Child.Ick);
            Assert.Equal("wow", localObject.Child.Ock);

        }


        [Fact]
        public void ParseCompleteSwagger()
        {
            DiagnosticListener listener = new DiagnosticListener("Testing");
            JsonStreamingParser.DiagSource = listener;
            listener.Subscribe(new ConsoleLogger(this.output));

            Vocabulary vocab = SwaggerVocab.Create();

            var stream = typeof(SwaggerParsingTests).Assembly
                .GetManifestResourceStream("SwaggerRouterTests.forecast.io.swagger.json");

            var swaggerDoc = new SwaggerDocument();

            JsonStreamingParser.ParseStream(stream, swaggerDoc, vocab);

            Assert.Equal("2.0", swaggerDoc.Version);
            Assert.Equal(1, swaggerDoc.Schemes.Count);
            Assert.Equal("https", swaggerDoc.Schemes.First());
            Assert.True(swaggerDoc.Paths.Count > 0);
            Assert.False(String.IsNullOrEmpty(swaggerDoc.Info.Description));
            Assert.False(String.IsNullOrEmpty(swaggerDoc.Info.Title));
            Assert.True(String.IsNullOrEmpty(swaggerDoc.Info.Version));
        }

    }

    // Objects with unknown keys
    // Arrays of Objects
    public class LocalType {
        public string Foo { get; set; }
        public int Baz { get; set; }
        public LocalChildType Child { get; set; }
        
    }

    public class LocalChildType
    {
        public string Ick { get; set; }
        public string Ock { get; set; }
    }

}
