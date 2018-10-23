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
using WebConsumer.Repositories;
using WebConsumer.Services;

namespace WebConsumer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("request")]
    public class RequestController : ApiController
    {
        private RequestRepository RequestRepo = new RequestRepository();

        [Route("submit")]
        [HttpPost]
        public IHttpActionResult SubmitRequest([FromBody] Request request)
        {
            var responseData = RequestExecutor.Execute(request);

            // TODO - Collect status and other info from the actual request in the executor
            return Ok(new GenericResponse
            {
                Status = HttpStatusCode.OK,
                Message = "Successfully submitted request",
                Data = responseData
            });
        }

        [Route("save")]
        [HttpPost]
        public object SaveRequest([FromBody] Request request)
        {
            return RequestRepo.Add(request);
        }
    }
}