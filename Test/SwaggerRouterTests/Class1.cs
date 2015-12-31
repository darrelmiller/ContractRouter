//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace SwaggerRouterTests
//{
//    public static class Program {

//        public static void Main() {

//            var jObject = new JObject(  new JProperty("swagger","2.0"),
//                                        new JProperty("x-unknown", "blah"),
//                                        new JProperty("info", 
//                                                new JObject(
//                                                        new JProperty("title","This is the title"),
//                                                        new JProperty("description","This is the description"),
//                                                        new JProperty("version","1.1")
//                                                        ))
//                                      );

//            //var stream = new MemoryStream();
//           // var x = typeof(Program).Assembly.GetManifestResourceNames();
//            var stream = typeof(Program).Assembly.GetManifestResourceStream("SwaggerRouterTests.forecast.io.swagger.json");

//         //   var sw = new StreamWriter(stream);
//           // sw.Write(jObject.ToString());
//           // sw.Flush();

//            //stream.Position = 0;

//            SwaggerStreamingParser.ParseStream(stream, new ConsoleSwaggerJsonConsumer());
//            Console.ReadLine();

//        }

//    }
  
//}
