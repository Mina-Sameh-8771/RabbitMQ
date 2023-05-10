using CommonValues;
using RabbitMQ.Client;
using System;

namespace WorkerQueue_Producer
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "WorkerQueue_Queue";
        static void Main(string[] args)
        {
            var payment1 = new Payment { AmountToPay = 1, CardNum = "1", Name = "Pay 1" };
            var payment2 = new Payment { AmountToPay = 2, CardNum = "2", Name = "Pay 2" };
            var payment3 = new Payment { AmountToPay = 3, CardNum = "3", Name = "Pay 3" };
            var payment4 = new Payment { AmountToPay = 4, CardNum = "4", Name = "Pay 4" };
            var payment5 = new Payment { AmountToPay = 5, CardNum = "5", Name = "Pay 5" };
            var payment6 = new Payment { AmountToPay = 6, CardNum = "6", Name = "Pay 6" };




            CreateConnection();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);
            SendMessage(payment4);
            SendMessage(payment5);
            SendMessage(payment6);

            Console.ReadLine();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, false, false, false, null);
        }


        private static void SendMessage(Payment payment)
        {
            _model.BasicPublish("", QueueName, null, payment.Serialize());
            Console.WriteLine("[x] Payment Message Send : Amount To Pay {0}", payment.AmountToPay);
        }
    }
}
