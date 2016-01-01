using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http.Routing;
using Tavis.UriTemplates;

namespace OpenApiRouter
{
    public class OpenApiRouter : IHttpRoute
    {
        private OpenApiDocument _OpenApiDoc;
        private UriTemplateTable _UriTemplateTable;

        public OpenApiRouter(Stream swaggerStream)
        {
            _OpenApiDoc = new OpenApiDocument();
            JsonStreamingParser.ParseStream(swaggerStream, _OpenApiDoc, OpenApiVocab.Create());

            _UriTemplateTable = new UriTemplateTable();
            foreach(var path in _OpenApiDoc.Paths)
            {
                if (!string.IsNullOrWhiteSpace(path.Value.XController))
                {
                    _UriTemplateTable.Add(path.Value.XController, new UriTemplate(path.Key));
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
            var routeData = new HttpRouteData(this);

            var match = _UriTemplateTable.Match(new Uri(request.RequestUri.PathAndQuery, UriKind.Relative));

            
            if (match == null) {
                return routeData;
            }
            
            routeData.Values.Add("controller", match.Key);
            foreach(var p in match.Parameters )
            {
                routeData.Values.Add(p.Key, p.Value);
            }
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
