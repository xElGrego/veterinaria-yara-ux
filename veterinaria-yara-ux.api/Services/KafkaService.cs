using veterinaria_yara_ux.application.interfaces.repositories;

namespace veterinaria_yara_ux.api.Services
{
    public class KafkaService : BackgroundService
    {
        private readonly INotificaciones _notificaciones;

        public KafkaService(INotificaciones notificaciones)
        {
            _notificaciones = notificaciones ?? throw new ArgumentNullException(nameof(notificaciones));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _notificaciones.Consumer(stoppingToken);
                await Task.CompletedTask;
                //await Task.Delay(1000, stoppingToken); // Agrega un retraso para evitar la sobrecarga
            }
        }
    }
}
