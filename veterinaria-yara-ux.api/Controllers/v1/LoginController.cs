using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.api.Controllers.v1
{
    [Tags("Login")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _usuariorRepository;
        public LoginController(ILogin usuariorRepository)
        {
            _usuariorRepository = usuariorRepository ?? throw new ArgumentNullException(nameof(usuariorRepository));
        }

        /// <summary>
        /// Método donde el usuario realiza el login
        /// </summary>
        ///<param name="usuario"> Objeto que se debe enviar para logearse </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<NuevoUsuarioDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/login")]
        public async Task<ActionResult<MsDtoResponse<NuevoUsuarioDTO>>> Login([FromBody][Required] UsuarioLogeoDTO usuario)

        {
            var response = await _usuariorRepository.Login(usuario);
            return Ok(new MsDtoResponse<NuevoUsuarioDTO>(response));
        }
    }
}
