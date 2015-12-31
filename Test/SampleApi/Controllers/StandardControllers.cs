using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var response = new HttpResponseMessage()
            {
                Content = new StringContent("Simple Resource")
            };
            return new ResponseMessageResult(response);
        }
    }

    public class ResourceClassController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent($"Resource {id} in class")
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
