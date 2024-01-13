using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.application.interfaces.repositories
{
    public interface ILogin
    {
        Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuario);
        Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario);
        Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken);
    }
}
