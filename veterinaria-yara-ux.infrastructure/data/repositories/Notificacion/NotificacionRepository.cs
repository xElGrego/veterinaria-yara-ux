using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.exceptions;
using veterinaria_yara_ux.domain.DTOs.Notificacion;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.infrastructure.data.repositories.Notificacion
{
    internal class NotificacionRepository : INotificaciones
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificacionRepository> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly IConsumer<string, string> _consumer;

        public NotificacionRepository(IConfiguration configuration, ILogger<NotificacionRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = _configuration.GetConnectionString("Kafka"),
                Acks = Acks.All,
            };

            _producer = new ProducerBuilder<string, string>(producerConfig).Build();

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _configuration.GetConnectionString("Kafka"),
                GroupId = "notificaciones-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        public void Consumer(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("employeeTopic");
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);

                    // Procesar el mensaje aquí
                    var key = consumeResult.Message.Key;
                    var value = consumeResult.Message.Value;
                    var data = JsonConvert.DeserializeObject<NotificacionDTO>(value);
                    _logger.LogInformation($"Key: {key}, Value: {value}");
                    _consumer.Commit(consumeResult);
                }
            }
            catch (Exception ex)
            {
                // El servicio ha sido cancelado
            }
            finally
            {
                _consumer.Close();
            }
        }

        public async Task<bool> Notificacion(NotificacionDTO messageParam)
        {
            try
            {
                var message = new Message<string, string>()
                {
                    Key = messageParam.IdNotificacion.ToString(),
                    Value = System.Text.Json.JsonSerializer.Serialize(messageParam)
                };

                await _producer.ProduceAsync("notificacionesTopic", message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notificar [" + JsonConvert.SerializeObject(messageParam) + "]");
                throw new VeterinariaYaraException(ex.Message);
            }
        }
    }
}
