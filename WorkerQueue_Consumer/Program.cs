using CommonValues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace WorkerQueue_Consumer
{
    class Program
    {

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "WorkerQueue_Queue";
        static void Main(string[] args)
        {
            Recieve();

            Console.ReadLine();
        }

        public static void Recieve()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();


            _model.QueueDeclare(QueueName, false, false, false, null);
            _model.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (model, ea) =>
            {
                var message = (Payment)ea.Body.ToArray().DeSerialize(typeof(Payment));
                _model.BasicAck(ea.DeliveryTag, false);

                Console.WriteLine(" [x] Received {0}", message.AmountToPay);
            };

            _model.BasicConsume(QueueName, false, consumer);

            

        }
    }
}
