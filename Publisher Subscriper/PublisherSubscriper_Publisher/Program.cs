using CommonValues;
using RabbitMQ.Client;
using System;

namespace PublisherSubscriper_Publisher
{
    class Program
    {

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "PublisherSubscriber_Exchange";

        static void Main(string[] args)
        {
            var payment1 = new Payment { AmountToPay = 1, CardNum = "1", Name = "Pay 1" };
            var payment2 = new Payment { AmountToPay = 2, CardNum = "2", Name = "Pay 2" };
            var payment3 = new Payment { AmountToPay = 3, CardNum = "3", Name = "Pay 3" };

            CreateConnection();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.ExchangeDeclare(ExchangeName , "fanout" ,false);
        }

        private static void SendMessage(Payment payment)
        {
            _model.BasicPublish(ExchangeName, "", null, payment.Serialize());
            Console.WriteLine("[x] Payment Message Send : AmountToPay {0}", payment.AmountToPay);
        }
    }
}
