using MongoOrm;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebConsumer.Models
{
    [DataContract]
    public class Request : MongoModel
    {
        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string Url { get; set; }

        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public Method Method { get; set; }

        [DataMember]
        public object RequestBody { get; set; }
        
        [DataMember]
        public string ContentType { get; set; }
    }

    public enum Method
    {
        Get,
        Post
    }
}