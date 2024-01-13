using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.domain.DTOs.Estados;


namespace veterinaria_yara_ux.infrastructure.data.repositories.Estados
{
    internal class EstadosRepository : IEstados
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EstadosRepository> _logger;
        public EstadosRepository(IConfiguration configuration, ILogger<EstadosRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<EstadosDTO>> ObtenerEstados()
        {
            var response = new List<EstadosDTO>();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "obtener-estados";

                using (var client = new HttpClient())
                {
                    var resTaxflash = await client.GetAsync(url_core);
                    if (resTaxflash.IsSuccessStatusCode)
                    {
                        var responseBody = await resTaxflash.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<List<EstadosDTO>>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obtener estados");
                throw;
            }
        }
    }
}
