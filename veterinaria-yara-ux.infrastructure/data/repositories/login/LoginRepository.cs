using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Usuario;

namespace veterinaria_yara_ux.infrastructure.data.repositories.login
{
    public class LoginRepository : ILogin
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginRepository> _logger;
        public LoginRepository(IConfiguration configuration, ILogger<LoginRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuario)
        {
            var response = new NuevoUsuarioDTO();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "login";

                using (var client = new HttpClient())
                {
                    var jsonBody = JsonConvert.SerializeObject(usuario);
                    var resCore = await client.PostAsync(url_core, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                    if (resCore.IsSuccessStatusCode)
                    {
                        var responseBody = await resCore.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<NuevoUsuarioDTO>(responseBody);
                        return response;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crear usuario [" + usuario + "]");
                throw;
            }
        }
    }
}
