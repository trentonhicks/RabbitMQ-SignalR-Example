using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "messages",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            Console.Write("Enter message:");

            while (true)
            {
                var message = Console.ReadLine();

                SendMessage(channel, message);

                Console.WriteLine("Message sent.\n");
            }
        }

        static void SendMessage(IModel channel, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "messages",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
