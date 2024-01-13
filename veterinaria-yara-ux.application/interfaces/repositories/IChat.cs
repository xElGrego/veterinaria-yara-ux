using veterinaria_yara_ux.domain.DTOs;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface IChat
    {
        Task Insertar(MensajeDTO idMascota);
    }
}
