using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace SwaggerRouter
{
    public class SwaggerRouter : IHttpRoute
    {
        public SwaggerRouter(Stream swaggerStream)
        {
            var reader = new JsonTextReader(new StreamReader(swaggerStream));
            
            while (reader.Read()) {
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        break;
                    case JsonToken.StartConstructor:
                        break;
                } 

                
            }
        }
        public IDictionary<string, object> Constraints
        {
            get { return new Dictionary<string, object>(); }
        }

        public IDictionary<string, object> DataTokens
        {
            get { return new Dictionary<string, object>(); }
        }

        public IDictionary<string, object> Defaults
        {
            get { return new Dictionary<string, object>(); }
        }

        public IHttpRouteData GetRouteData(string virtualPathRoot, System.Net.Http.HttpRequestMessage request)
        {
            return new HttpRouteData(this) { 
            };
        }

        public IHttpVirtualPathData GetVirtualPath(System.Net.Http.HttpRequestMessage request, IDictionary<string, object> values)
        {
            return null;
        }

        public System.Net.Http.HttpMessageHandler Handler
        {
            get { return null; }
        }

        public string RouteTemplate
        {
            get;
            set;
        }
    }
}
