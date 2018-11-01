using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using WebConsumer.Models;

namespace WebConsumer.Services
{
    public class RequestExecutor
    {
        public static GenericResponse Execute(Request request)
        {
            var client = new RestClient(request.Url);
            RestRequest req = null;

            switch (request.Method)
            {
                case Models.Method.Post:
                    req = new RestRequest(RestSharp.Method.POST);
                    break;
                case Models.Method.Get:
                    req = new RestRequest(RestSharp.Method.GET);
                    break;
            }

            if (request.Method.Equals(Models.Method.Post))
            {
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(request.RequestBody));
                req.AddHeader("Content-Type", request.ContentType ?? "application/json");
                req.AddParameter("undefined", data, ParameterType.RequestBody);
            }

            if (request.AuthType.Equals(AuthType.OAuth2))
            {
                var authClient = new RestClient(request.AuthUrl);
                var authRequest = new RestRequest(RestSharp.Method.POST);
                authRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var encodedAuth = Encoding.UTF8.GetBytes($"{request.ApiKey}:{request.ApiSecret}");
                authRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(encodedAuth)}");

                authRequest.AddParameter("undefined", "grant_type=client_credentials", ParameterType.RequestBody);

                IRestResponse authResponse = authClient.Execute(authRequest);
                var authData = (JObject)JsonConvert.DeserializeObject(authResponse.Content);

                string accessToken = authData["access_token"].Value<string>();

                req.AddHeader("Authorization", $"Bearer {accessToken}");
            }

            GenericResponse genericResponse = new GenericResponse();

            try
            {
                IRestResponse response = client.Execute(req);

                genericResponse.Status = response.StatusCode;
                genericResponse.ContentType = response.ContentType;
                genericResponse.Data = response.Content;
            }
            catch (Exception e)
            {
                if (e is WebException webException)
                {
                    var exResponse = (HttpWebResponse)webException.Response;
                    genericResponse.Status = exResponse.StatusCode;
                    genericResponse.ContentType = exResponse.ContentType;
                    genericResponse.Data = new StreamReader(exResponse.GetResponseStream()).ReadToEnd();
                }
                else
                {
                    genericResponse.Status = HttpStatusCode.InternalServerError;
                    genericResponse.Message = e.Message;
                }
            }

            return genericResponse;
        }

    }
}