using Newtonsoft.Json;
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
        public static object Execute(Request request)
        {
            var req = (HttpWebRequest)WebRequest.Create(request.Url);
            req.Method = request.Method.ToString().ToUpper();

            if (request.Method.Equals(Method.Post))
            {
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(request.RequestBody));

            
                req.ContentType = "application/json";
                req.ContentLength = data.Length;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            var response = (HttpWebResponse)req.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

    }
}