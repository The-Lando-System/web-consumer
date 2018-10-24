using System;
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
        [RequestValidator]
        public IHttpActionResult ExecuteRequest([FromBody] Request request)
        {
            return Ok(RequestExecutor.Execute(request));
        }

        [Route("execute/{id}")]
        [HttpPost]
        [RequestValidator]
        public IHttpActionResult ExecuteStoredRequest(string id)
        {
            Request request = RequestRepo.FindById(id);

            if (request == null)
            {
                return BadRequest($"Could not find a stored request with ID of [{id}]");
            }

            return Ok(RequestExecutor.Execute(request));
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