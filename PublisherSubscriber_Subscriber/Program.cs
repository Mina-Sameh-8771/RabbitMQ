using CommonValues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace PublisherSubscriber_Subscriber
{
    class Program
    {

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "PublisherSubscriber_Exchange";

        static void Main(string[] args)
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            var qeueuName = DeclareAndBindingQueueToExchange();
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (model, ea) =>
            {
                var message = ea.Body.ToArray().DeSerialize(typeof(Payment));
                Console.WriteLine(" [x] Received {0}", message);
            };

            _model.BasicConsume(qeueuName, true, consumer);

            Console.ReadLine();
        }

        public static string DeclareAndBindingQueueToExchange()
        {
            _model.ExchangeDeclare(ExchangeName, "fanout");
            var qeueuName = _model.QueueDeclare().QueueName;
            _model.QueueBind(qeueuName, ExchangeName, "");
            return qeueuName;
        }
    }
}
