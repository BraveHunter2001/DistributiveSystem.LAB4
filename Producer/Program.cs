
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
var random = new Random();

channel.QueueDeclare(queue: "Lab4",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

for (int i = 0; i < 50; i++)
{
    string message = $"Now time is {DateTime.Now}";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "Lab4",
                         basicProperties: null,
                         body: body);
    Console.WriteLine($" [x] Sent {message}");
    Thread.Sleep(random.Next(10,500));
}



Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();