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
        private SwaggerDocument swaggerDoc;
        public SwaggerRouter(Stream swaggerStream)
        {
            swaggerDoc = new SwaggerDocument();
            JsonStreamingParser.ParseStream(swaggerStream, swaggerDoc, SwaggerVocab.Create());
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
            var routeData = new HttpRouteData(this);

            Path path = null;
            var requestPath = request.RequestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
            if (!swaggerDoc.Paths.TryGetValue("/"+requestPath, out path)) {
                return routeData;
            }
            
            routeData.Values.Add("controller", path.Operations[request.Method.ToString().ToLower()].Id);

            return routeData;
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
