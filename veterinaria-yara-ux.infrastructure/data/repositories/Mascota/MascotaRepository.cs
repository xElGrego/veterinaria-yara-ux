using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Estados;
using veterinaria_yara_ux.domain.DTOs.Mascota;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace veterinaria_yara_ux.infrastructure.data.repositories.Mascota
{
    public class MascotaRepository : IMascota
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MascotaRepository> _logger;
        public MascotaRepository(IConfiguration configuration, ILogger<MascotaRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task<CrearResponse> ActivarMascota(Guid idMascota)
        {
            throw new NotImplementedException();
        }

        public Task<MascotaDTO> ConsultarMascotaId(Guid idMascota)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotas(int start, int length, string nombre, int estado, DateTime fechaInicio, DateTime fechaFin, Guid? idUsuario, CancellationToken cancellationToken)
        {
            var response = new PaginationFilterResponse<MascotaDTO>();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "consulta-mascotas?start=" + start + "&lenght=" + length + "&nombre=" + nombre + "&estado=" + estado + "&fechaInicio=" + fechaInicio.ToString("yyyy-MM-dd") + "&fechaFin=" + fechaFin.ToString("yyyy-MM-dd") + "&idUsuario=" + idUsuario;

                using (var client = new HttpClient())
                {
                    var resTaxflash = await client.GetAsync(url_core);
                    if (resTaxflash.IsSuccessStatusCode)
                    {
                        var responseBody = await resTaxflash.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<PaginationFilterResponse<MascotaDTO>>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obtener mascotas");
                throw;
            }
        }

        public async Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotasUsuario(int start, int length, Guid idUsuario, CancellationToken cancellationToken)
        {
            var response = new PaginationFilterResponse<MascotaDTO>();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "consulta-mascotas-usuarios?start=" + start + "&length=" + length + "&idUsuario=" + idUsuario;

                using (var client = new HttpClient())
                {
                    var resTaxflash = await client.GetAsync(url_core);
                    if (resTaxflash.IsSuccessStatusCode)
                    {
                        var responseBody = await resTaxflash.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<PaginationFilterResponse<MascotaDTO>>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultar mascotas usuarios");
                throw;
            }
        }

        public async Task<CrearResponse> CrearMascota(NuevaMascotaDto mascota)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "crear-mascota";

                using (var client = new HttpClient())
                {
                    var jsonBody = JsonConvert.SerializeObject(mascota);
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
                _logger.LogError(ex, "Error crear raza [" + JsonConvert.SerializeObject(mascota) + "]");
                throw;
            }
        }

        public async Task<CrearResponse> EditarMascota(MascotaDTO mascota)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "editar-mascota";

                using (var client = new HttpClient())
                {
                    var jsonBody = JsonConvert.SerializeObject(mascota);
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
                _logger.LogError(ex, "Error editar mascota [" + JsonConvert.SerializeObject(mascota) + "]");
                throw;
            }
        }

        public async Task<CrearResponse> EliminarMascota(Guid idMascota)
        {
            var response = new CrearResponse();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "eliminar-mascota";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("idMascota", idMascota.ToString());
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
                _logger.LogError(ex, "Error eliminar raza [" + JsonConvert.SerializeObject(idMascota) + "]");
                throw;
            }
        }

        public Task<CrearResponse> ReordenarMascota(List<ReordenarMascotaDTO> mascotas)
        {
            throw new NotImplementedException();
        }

        public Task<MascotaDTO> UltimaMascota(Guid idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
