using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebConsumer.Models;

namespace WebConsumer.Repositories
{
    public class RequestRepository : MongoRepository<Request>
    {
        public RequestRepository() : base() { }
    }
}