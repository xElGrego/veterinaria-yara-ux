using veterinaria_yara_ux.domain.DTOs.Notificacion;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface INotificaciones
    {
        Task<bool> Notificacion(NotificacionDTO message);
    }
}
