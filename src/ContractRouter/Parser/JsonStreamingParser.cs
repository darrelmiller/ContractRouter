using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractRouter
{
    

    public class JsonStreamingParser
    {
        public static DiagnosticSource DiagSource {
            get; set;
        }

        public static void ParseStream(Stream stream, object rootSubject, Vocabulary termHandlers)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            var ostack = new Stack<Context>();
            Context currentContext = null;
            string currentProperty = "";

            Func<Context, object, Context> termHandler = null;

            while (reader.Read())
            {
                
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        
                        if (currentContext == null)
                        {
                            currentContext = new Context()
                            {
                                Subject = rootSubject,
                                Term = ""
                            };
                        }
                        else {
                            ostack.Push(currentContext);
                            if (termHandler != null)
                            {
                                var newContext = termHandler(currentContext, currentProperty);
                                if (newContext != null)  // Not an array
                                {
                                    currentContext = newContext;
                                }
                            }
                            
                        }
                        DiagSource?.Write("Tavis.JsonStreamingParser.NewObject", currentContext.Term);
                        break;
                    case JsonToken.StartArray:
                        break;
                    case JsonToken.EndArray:
                        break;
                    case JsonToken.EndObject:
                        DiagSource?.Write("Tavis.JsonStreamingParser.EndObject", currentContext.Term);
                        if (ostack.Count > 0)
                        {
                            currentContext = ostack.Pop();
                        }
                        break;

                    case JsonToken.PropertyName:
                        currentProperty = reader.Value.ToString();
                        termHandler = termHandlers.FindHandler(currentContext.Term, currentProperty);
                        break;
                    case JsonToken.Integer:
                    case JsonToken.Boolean:
                    case JsonToken.String:
                        if (termHandler != null)
                        {
                            termHandler(currentContext, reader.Value);
                        } else
                        {
                            DiagSource?.Write("Tavis.JsonStreamingParser.MissingTermHandler", new { Context = currentContext, Property = currentProperty });
                        }
                        break;
                }
            }
        }


    }

}
