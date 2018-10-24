using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebConsumer.Filters;
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

        [Route("execute")]
        [HttpPost]
        public IHttpActionResult ExecuteRequest([FromBody] Request request)
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

        [Route("")]
        [HttpPost]
        [RequestValidator]
        public IHttpActionResult SaveRequest([FromBody] Request request)
        {
            if (RequestRepo.FindByProperty("Name", request.Name) != null)
            {
                return BadRequest($"Request with name [{request.Name}] already exists");
            }

            Request createdRequest = null;

            try
            {
                createdRequest = RequestRepo.Add(request);
            }
            catch (Exception e)
            {
                Exception exception = new Exception("Failed to save new request", e);
                return InternalServerError(exception);
            }

            if (createdRequest == null)
            {
                Exception exception = new Exception("Failed to save new request");
                return InternalServerError(exception);
            }

            return Ok(createdRequest);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllRequests()
        {
            return Ok(RequestRepo.FindAll());
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetRequestById(string id)
        {
            return Ok(RequestRepo.FindById(id));
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteRequest(string id)
        {
            return Ok(RequestRepo.Delete(id));
        }

        [Route("{id}")]
        [HttpPut]
        [RequestValidator]
        public IHttpActionResult EditRequest(string id, [FromBody] Request request)
        {
            return Ok(RequestRepo.Update(id, request));
        }
    }
}