using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Raza;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.infrastructure.data.repositories.Raza
{
    public class RazaRepository : IRaza
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RazaRepository> _logger;
        public RazaRepository(IConfiguration configuration, ILogger<RazaRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<RazaDTO> ConsultarRazaId(Guid idRaza)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationFilterResponse<RazaDTO>> ConsultarRazas(string buscar, int start, int length)
        {
            var response = new PaginationFilterResponse<RazaDTO>();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "consulta-razas?buscar=" + buscar + "&start=" + start + "&length=" + length;

                using (var client = new HttpClient())
                {
                    var resTaxflash = await client.GetAsync(url_core);
                    if (resTaxflash.IsSuccessStatusCode)
                    {
                        var responseBody = await resTaxflash.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<PaginationFilterResponse<RazaDTO>>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultar razas");
                throw;
            }
        }

        public async Task<CrearResponse> CrearRaza(NuevaRazaDTO raza)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "crear-raza";

                using (var client = new HttpClient())
                {
                    var jsonBody = JsonConvert.SerializeObject(raza);
                    var resCore = await client.PostAsync(url_core, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                    if (resCore.IsSuccessStatusCode)
                    {
                        var responseBody = await resCore.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<CrearResponse>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crear raza [" + JsonConvert.SerializeObject(raza) + "]");
                throw;
            }
        }

        public async Task<CrearResponse> EditarRaza(RazaDTO raza)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "editar-raza";

                using (var client = new HttpClient())
                {
                    var jsonBody = JsonConvert.SerializeObject(raza);
                    var resCore = await client.PutAsync(url_core, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                    if (resCore.IsSuccessStatusCode)
                    {
                        var responseBody = await resCore.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<CrearResponse>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crear raza [" + JsonConvert.SerializeObject(raza) + "]");
                throw;
            }
        }

        public async Task<CrearResponse> EliminarRaza(Guid idRaza)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "eliminar-raza";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("idRaza", idRaza.ToString());
                    var resCore = await client.DeleteAsync(url_core);

                    if (resCore.IsSuccessStatusCode)
                    {
                        var responseBody = await resCore.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<CrearResponse>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crear raza [" + JsonConvert.SerializeObject(idRaza) + "]");
                throw;
            }
        }


        public async Task<List<RazaDTO>> ObtenerRazas()
        {
            var response = new List<RazaDTO>();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "obtener-razas?";

                using (var client = new HttpClient())
                {
                    var resCore = await client.GetAsync(url_core);
                    if (resCore.IsSuccessStatusCode)
                    {
                        var responseBody = await resCore.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<List<RazaDTO>>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obtener razas");
                throw;
            }
        }
    }
}
