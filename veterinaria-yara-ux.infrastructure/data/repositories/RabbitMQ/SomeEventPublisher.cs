using MassTransit;
using veterinaria_yara_ux.domain.DTOs.RabbitMQ;

namespace veterinaria_yara_ux.infrastructure.data.repositories.RabbitMQ
{
    public class SomeEventPublisher
    {
        private readonly IBus _bus;

        public SomeEventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishUserCreatedEvent(string userName)
        {
            var userCreatedMessage = new UserCreated
            {
                UserName = userName,
                Id = Guid.NewGuid(),
            };
            var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/test-gregorito-queue"));
            await endpoint.Send(userCreatedMessage);
        }
    }
}
