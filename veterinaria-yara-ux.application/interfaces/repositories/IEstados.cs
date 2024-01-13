using veterinaria_yara_ux.domain.DTOs.Estados;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface IEstados
    {
        Task<List<EstadosDTO>> ObtenerEstados();
    }
}
