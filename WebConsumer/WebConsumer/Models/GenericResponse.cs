using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace WebConsumer.Models
{
    [DataContract]
    public class GenericResponse
    {
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpStatusCode Status { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public object Data { get; set; }
    }
}