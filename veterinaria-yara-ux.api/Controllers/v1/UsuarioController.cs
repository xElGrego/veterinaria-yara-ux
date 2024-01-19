using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Estados.Mascota;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Raza;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.api.Controllers.v1
{
    [Tags("Usuario")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuariorRepository;

        public UsuarioController(IUsuario usuario)
        {
            _usuariorRepository = usuario ?? throw new ArgumentNullException(nameof(usuario));
        }

        /// <summary>
        /// Método donde el usuario realiza el login
        /// </summary>
        ///<param name="usuario"> Objeto que se debe enviar para logearse </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(NuevoUsuarioDTO), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/login")]
        public async Task<ActionResult<NuevoUsuarioDTO>> Login([FromBody][Required] UsuarioLogeoDTO usuario)

        {
            var response = await _usuariorRepository.Login(usuario);
            return Ok(new DtoResponse<NuevoUsuarioDTO>(response));
        }

        [HttpPost]
        //[Authorize(Policy = "SuperAdministrador")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-usuario")]
        public async Task<ActionResult<DtoResponse<CrearResponse>>> CrearUsuario([FromBody][Required] AgregarUsuarioDTO usuario)
        {
            var response = await _usuariorRepository.CrearUsuario(usuario);
            return Ok(new DtoResponse<CrearResponse>(response));
        }

        /// <summary>
        /// Genera la lista de razas paginadas
        /// </summary>
        /// <param name="start"> Número de páginan dodne se requiere empezar la consulta </param>
        /// <param name="length"> Cantidad de items que se requiere obtener </param>
        /// <param name="nombre"> Nombre de la mascota a buscar </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DtoResponse<PaginationFilterResponse<UsuarioDTO>>), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-usuarios")]
        public async Task<ActionResult<PaginationFilterResponse<UsuarioDTO>>> ConsultarMascotasUsuario(int start, Int16 length, Guid idUsuario,
        CancellationToken cancellationToken)
        {
            var response = await _usuariorRepository.ConsultarUsuarios(start, length, cancellationToken);
            return Ok(new DtoResponse<PaginationFilterResponse<UsuarioDTO>>(response));
        }
    }
}
