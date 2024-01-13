using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Raza;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface IRaza
    {
        Task<List<RazaDTO>> ObtenerRazas();
        Task<PaginationFilterResponse<RazaDTO>> ConsultarRazas(string buscar, int start, int lenght);
        Task<RazaDTO> ConsultarRazaId(Guid idRaza);
        Task<CrearResponse> CrearRaza(NuevaRazaDTO raza);
        Task<CrearResponse> EditarRaza(RazaDTO raza);
        Task<CrearResponse> EliminarRaza(Guid idRaza);
    }
}
