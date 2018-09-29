using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebConsumer.Models;
using WebConsumer.Services;

namespace WebConsumer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("request")]
    public class RequestController : ApiController
    {
        [Route("submit")]
        [HttpPost]
        public GenericResponse SubmitRequest([FromBody] Request request)
        {
            var responseData = RequestExecutor.Execute(request);

            return new GenericResponse
            {
                Status = HttpStatusCode.OK,
                Message = "Successfully submitted request",
                Data = responseData
            };
        }

        [Route("post-test")]
        [HttpPost]
        public object PostTest([FromBody] object testBody)
        {
            return testBody;
        }
    }
}