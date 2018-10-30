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
            var req = (HttpWebRequest)WebRequest.Create(request.Url);
            req.Method = request.Method.ToString().ToUpper();

            if (request.Method.Equals(Models.Method.Post))
            {
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(request.RequestBody));
            
                req.ContentType = req.ContentType ?? "application/json";
                req.ContentLength = data.Length;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            if (request.AuthType.Equals(AuthType.OAuth2))
            {
                var client = new RestClient(request.AuthUrl);
                var authRequest = new RestRequest(RestSharp.Method.POST);
                authRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var encodedAuth = Encoding.UTF8.GetBytes($"{request.ApiKey}:{request.ApiSecret}");
                authRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(encodedAuth)}");

                authRequest.AddParameter("undefined", "grant_type=client_credentials", ParameterType.RequestBody);

                IRestResponse authResponse = client.Execute(authRequest);
                var authData = (JObject)JsonConvert.DeserializeObject(authResponse.Content);

                string accessToken = authData["access_token"].Value<string>();

                req.Headers["Authorization"] = $"Bearer {accessToken}";
            }

            GenericResponse genericResponse = new GenericResponse();
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)req.GetResponse();

                genericResponse.Status = response.StatusCode;
                genericResponse.ContentType = response.ContentType;
                genericResponse.Data = new StreamReader(response.GetResponseStream()).ReadToEnd();
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