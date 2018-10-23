using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace WebConsumer.Models
{
    [DataContract]
    public class MongoModel
    {
        [DataMember]
        [BsonId]
        public string Id { get; set; }

        [DataMember]
        public DateTime LastModified { get; set; }
    }
}