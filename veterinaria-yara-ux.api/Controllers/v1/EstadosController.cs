using Microsoft.AspNetCore.Mvc;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.domain.DTOs.Estados;

namespace veterinaria_yara_ux.api.Controllers.v1
{
    [Tags("Estados")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class EstadosController : BaseApiController
    {
        private readonly IEstados _estados;
        public EstadosController(IEstados estados)
        {
            _estados = estados ?? throw new ArgumentNullException(nameof(estados));
        }

        /// <summary>
        /// Método para obtener los estados disponibles de razas y usurarios
        /// </summary>
        /// 
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<List<EstadosDTO>>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/obtener-estados")]
        public async Task<ActionResult<List<EstadosDTO>>> ObtenerEstados()
        {
            var response = await _estados.ObtenerEstados();
            return Ok(new MsDtoResponse<List<EstadosDTO>>(response));
        }
    }
}
