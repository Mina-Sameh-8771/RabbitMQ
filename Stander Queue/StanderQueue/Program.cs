using CommonValues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace StanderQueue
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "StanerQueue_ExampleQueue";


        static void Main(string[] args)
        {

            var payment1 = new Payment { AmountToPay = 1, CardNum = "1", Name = "Pay 1" };
            var payment2 = new Payment { AmountToPay = 2, CardNum = "2", Name = "Pay 2" };
            var payment3 = new Payment { AmountToPay = 3, CardNum = "3", Name = "Pay 3" };

            CreateQueue();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);

            Recieve();

            SendMessage(payment1);

            Console.ReadLine();


        }

        private static void CreateQueue()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, false, false, false, null);
        }

        private static void SendMessage(Payment payment)
        {
            _model.BasicPublish("", QueueName, null, payment.Serialize());
            Console.WriteLine("[x] Payment Message Send : AmountToPay {0}" , payment.AmountToPay);
        }

        private static void Recieve()
        {
            var consumer = new EventingBasicConsumer(_model);

            

            consumer.Received += (model, ea) =>
            {
                var message = ea.Body.ToArray().DeSerialize(typeof(Payment));
                Console.WriteLine(" [x] Received {0}", message);
            };

            _model.BasicConsume(QueueName, true, consumer);

        }
    }
}
