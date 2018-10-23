using MongoOrm;
using WebConsumer.Models;

namespace WebConsumer.Repositories
{
    public class RequestRepository : MongoRepository<Request>
    {
        public RequestRepository() : base() { }
    }
}