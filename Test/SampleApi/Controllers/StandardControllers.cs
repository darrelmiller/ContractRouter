using ContractRouter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace SampleApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            var response = new HttpResponseMessage()
            {
                    Content = new StringContent("Hello World")
            };
            return new ResponseMessageResult(response);
        }
    }

    public class ResourceController : ApiController
    {
        public IHttpActionResult Get()
        {

            var link = this.GetContractUrl<ResourceController>(new Dictionary<string, object>());

            var response = new HttpResponseMessage()
            {
                Content = new StringContent("Simple Resource : " + link.OriginalString)
            };

            return new ResponseMessageResult(response);
        }
    }

    public class ResourceClassController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var url = this.GetContractUrl<ResourceClassController>(new Dictionary<string, object>() { { "id",id}  });

            var response = new HttpResponseMessage()
            {
                Content = new StringContent($"Resource {id} in class : " + url.OriginalString)
            };
            return new ResponseMessageResult(response);
        }
    }

    public class CollectionController : ApiController
    {
        public IHttpActionResult Get(string keyword)
        {
            var response = new HttpResponseMessage()
            {

            };
            return new ResponseMessageResult(response);
        }
    }

    public class ChildCollectionController : ApiController
    {
        public IHttpActionResult Get(int resourceId, string keyword)
        {
            var response = new HttpResponseMessage()
            {

            };
            
            return new ResponseMessageResult(response);
        }

    }
}
