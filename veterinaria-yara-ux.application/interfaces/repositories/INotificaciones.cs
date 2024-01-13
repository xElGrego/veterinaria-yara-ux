namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface INotificaciones
    {
        Task<bool> NotificacionOfertas(string message);
        Task<bool> NotificacionUsuario(string message, Guid idUsuario);
    }
}
