using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;

namespace WebConsumer.Models
{
    [DataContract]
    public class Request : MongoModel
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public Method Method { get; set; }

        [DataMember]
        public object RequestBody { get; set; }
        
    }

    public enum Method
    {
        Get,
        Post
    }
}