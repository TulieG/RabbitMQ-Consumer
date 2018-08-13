
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;

    namespace CoreReceive1
    {
        class Program
        {
            static void Main(string[] args)
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "Oloza2013" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Name",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var name = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Hello {0}", name);
                    };
                    channel.BasicConsume(queue: "Name", autoAck: true, consumer: consumer);

                    Console.WriteLine("press any key to exit.");
                    Console.ReadKey();

                }
            }
        }
    }



