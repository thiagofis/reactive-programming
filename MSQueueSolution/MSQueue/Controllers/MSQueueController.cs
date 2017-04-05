using System.IO;
using System.Messaging;
using System.Text;
using System.Web.Http;
using MSQueue.Models;
using Newtonsoft.Json;
using Ploeh.AutoFixture;

namespace MSQueue.Controllers
{
    [RoutePrefix("api/msqueue")]
    public class MSQueueController : ApiController
    {
        private readonly Fixture fixture;

        public MSQueueController()
        {
            fixture = new Fixture();
        }

        [HttpPost, Route("xml")]
        public string Xml()
        {
            var model = fixture.Create<QueueModel>();

            using (var queue = new MessageQueue(".\\private$\\transactional-queue"))
            {
                var message = new Message(model)
                {
                    Label = $"Message Label XML {fixture.Create<int>()}",
                    Priority = MessagePriority.Highest
                };

                var transactional = new MessageQueueTransaction();

                transactional.Begin();
                queue.Send(message, transactional);
                transactional.Commit();
            }

            return "OK!";
        }

        [HttpPost, Route("json")]
        public string Json()
        {
            var model = fixture.Create<QueueModel>();

            using (var queue = new MessageQueue(".\\private$\\transactional-queue"))
            {

                var jsonBody = JsonConvert.SerializeObject(model);

                var stream = new MemoryStream(Encoding.Default.GetBytes(jsonBody));

                var message = new Message()
                {
                    Label = $"Message Label JSON {fixture.Create<int>()}",
                    Priority = fixture.Create<MessagePriority>(),
                    BodyStream = stream
                };

                var transactional = new MessageQueueTransaction();

                transactional.Begin();
                queue.Send(message, transactional);
                transactional.Commit();
            }

            return "OK!";
        }
    }
}