using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using TaskManagerSystem.Infra.Adapter.Configuration;

namespace TaskManagerSystem.Infra.Adapter.RabbitMQ
{
    public class RabbitProvider : IDisposable
    {
        private static RabbitProvider _instance = null;

        private IConnection _connection;
        private IModel _channel;
        private readonly RabbitMQConfiguration _rabbitConfig;

        private static class ExchangeType
        {
            public const string Direct = "direct";
            public const string Fanout = "fanout";
            public const string Headers = "headers";
            public const string Topic = "topic";
        }

        protected RabbitProvider(RabbitMQConfiguration configuration)
        {
            _rabbitConfig = configuration;
            var factory = new ConnectionFactory() {
                HostName = _rabbitConfig.Host,
                Port = _rabbitConfig.Port,
                UserName = _rabbitConfig.UserName,
                Password = _rabbitConfig.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public static RabbitProvider Build(RabbitMQConfiguration configuration)
        {
            if (_instance == null)
            {
                _instance = new RabbitProvider(configuration).OneTimeSetup();
            }
            return _instance;
        }

        public RabbitProvider OneTimeSetup()
        {
            this.WithQueue();
            return this;
        }

        public RabbitProvider WithQueue()
        {
            _channel.QueueDeclare(queue: _rabbitConfig.Queue,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            return this;
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        //public RabbitProvider WithExchange()
        //{
        //    _channel.ExchangeDeclare(exchange: _rabbitConfig.Exchange,
        //                             type: ExchangeType.Direct,
        //                             durable: true,
        //                             autoDelete: false);
        //    return this;
        //}

        //public RabbitProvider WithBind()
        //{
        //    _channel.QueueBind(queue: _rabbitConfig.Queue,
        //                       exchange: _rabbitConfig.Exchange,
        //                       routingKye: _rabbitConfig.RoutingKey);
        //    return this;
        //}

        public RabbitProvider Publish<T>(T message)
        {
            var byteMessage = ConvertMessageToByteArray(message);

            //_channel.BasicPublish(exchange: _rabbitConfig.Exchange,
            //                      routingKey: _rabbitConfig.RoutingKey,
            //                      basicProperties: null,

            _channel.BasicPublish(exchange: _rabbitConfig.Exchange,
                                  routingKey: _rabbitConfig.Queue,
                                  basicProperties: null,
                                  body: byteMessage);
            return this;
        }

        private byte[] ConvertMessageToByteArray<T>(T message)
        {
            var strMessage = "";

            if (message is string)
            {
                strMessage = (message as string);
            }
            else
            {
                strMessage = JsonConvert.SerializeObject(message);
            }
            return Encoding.UTF8.GetBytes(strMessage);
        }

        public TResult ConvertByteArrayToMessage<TResult>(Byte[]? body)
        {
            if (body != null)
            {
                var message = Encoding.UTF8.GetString(body);
                return JsonConvert.DeserializeObject<TResult>(message);
            }
            else
                return default(TResult);
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}