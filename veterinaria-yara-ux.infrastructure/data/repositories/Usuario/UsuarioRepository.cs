using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.application.models.dtos;
using veterinaria_yara_ux.application.models.exceptions;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Paginador;
using veterinaria_yara_ux.domain.DTOs.Usuario;
using veterinaria_yara_ux.infrastructure.data.repositories.Raza;

namespace veterinaria_yara_ux.infrastructure.data.repositories.Usuario
{
    public class UsuarioRepository : IUsuario
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioRepository> _logger;


        public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<PaginationFilterResponse<UsuarioDTO>> ConsultarUsuarios(int start, int length, CancellationToken cancellationToken)
        {
            PaginationFilterResponse<UsuarioDTO> usuarios = new();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "consultar-usuarios?" + "start=" + start + "&length=" + length;
                using (var client = new HttpClient())
                {
                    var res = await client.GetAsync(url_core);
                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        usuarios = JsonConvert.DeserializeObject<PaginationFilterResponse<UsuarioDTO>>(json);
                    }
                    else
                    {
                        var errorMessage = await res.Content.ReadAsStringAsync();
                        var error = JsonConvert.DeserializeObject<DtoResponseError>(errorMessage);
                        throw new VeterinariaYaraException(error.message);
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar usuarios", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
        }

        public async Task<CrearResponse> CrearUsuario(AgregarUsuarioDTO usuario)
        {
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "crear-usuario";
                string? json = JsonConvert.SerializeObject(usuario);
                StringContent contentJSON = new(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    var res = await client.PostAsync(url_core, contentJSON);
                    if (res.IsSuccessStatusCode)
                    {
                        var recep = await res.Content.ReadAsStringAsync();
                        _logger.LogInformation("Respuesta crear usuario [" + JsonConvert.SerializeObject(recep) + "]");
                        await Notificar(usuario);
                    }
                    else
                    {
                        var errorMessage = await res.Content.ReadAsStringAsync();
                        var error = JsonConvert.DeserializeObject<DtoResponseError>(errorMessage);
                        throw new VeterinariaYaraException(error.message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error crear usuario [" + JsonConvert.SerializeObject(usuario) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "El usuario fue creado con éxito"
            };
            return response;
        }

        public async Task<NuevoUsuarioDTO> Login(UsuarioLogeoDTO usuarioP)
        {
            var usuario = new NuevoUsuarioDTO();
            try
            {
                var url_core = _configuration.GetConnectionString("VeterinariaCore") + "login";
                string? json = JsonConvert.SerializeObject(usuarioP);
                StringContent contentJSON = new(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    var res = await client.PostAsync(url_core, contentJSON);
                    if (res.IsSuccessStatusCode)
                    {
                        var recep = await res.Content.ReadAsStringAsync();
                        usuario = JsonConvert.DeserializeObject<NuevoUsuarioDTO>(recep);
                    }
                    else
                    {
                        var errorMessage = await res.Content.ReadAsStringAsync();
                        var error = JsonConvert.DeserializeObject<DtoResponseError>(errorMessage);
                        throw new VeterinariaYaraException(error.message);
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError("Login ", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
        }

        public async Task<bool> Notificar(AgregarUsuarioDTO message)
        {
            try
            {
                var routingKey = "veterinaria.notificaciones";
                var hostName = _configuration["RabbitMQ:HostName"];
                var userName = _configuration["RabbitMQ:UserName"];
                var passWord = _configuration["RabbitMQ:PassWord"];

                var factory = new ConnectionFactory
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = passWord,
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var serializedMessage = JsonConvert.SerializeObject(message);
                        var body = Encoding.UTF8.GetBytes(serializedMessage);
                        var properties = channel.CreateBasicProperties();
                        channel.BasicPublish(exchange: "veterinaria", routingKey: routingKey, basicProperties: properties, body: body);
                        _logger.LogInformation("Usuario enviando con exito");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al enviar mensaje topic: " + ex.Message);
                throw new Exception("Error al enviar mensaje topic");
            }
        }
    }
}
