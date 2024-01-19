using veterinaria_yara_ux.domain.DTOs.Usuario;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface IUsuario
    {
        Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuario);
        Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario);
        Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken);
    }
}
