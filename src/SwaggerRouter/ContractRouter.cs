using Hapikit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http.Routing;
using Tavis.UriTemplates;

namespace ContractRouter
{
    public class ContractRouter : IHttpRoute
    {
        private UriTemplateTable _UriTemplateTable;

        public ContractRouter(Stream contractStream)
        { 
            // By default, assume it is an OpenAPI contract
            // We can add another ctor that has a media type parameter to support
            // other contracts.

            var openApiDoc = new OpenApiDocument();
            JsonStreamingParser.ParseStream(contractStream, openApiDoc, OpenApiVocab.Create());

            _UriTemplateTable = new UriTemplateTable();
            foreach(var path in openApiDoc.Paths)
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
