using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.domain.DTOs.Notificacion;
using veterinaria_yara_ux.domain.DTOs.Paginador;

namespace veterinaria_yara_ux.api.Controllers.v1
{
    [Tags("Notificacion")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificaciones _notificacion;
        public NotificacionController(INotificaciones notificacion)
        {
            _notificacion = notificacion ?? throw new ArgumentNullException(nameof(notificacion));
        }

        /// <summary>
        /// Genera la lista de razas paginadas
        /// </summary>
        /// <param name="mensaje"> Mensaje </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DtoResponse<PaginationFilterResponse<bool>>), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-notificacion")]
        public async Task<ActionResult<bool>> ConsultarMascotas([FromBody] NotificacionDTO mensaje)
        {
            var response = await _notificacion.Notificacion(mensaje);
            return Ok(new DtoResponse<bool>(response));
        }
    }
}
