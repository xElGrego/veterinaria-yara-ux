using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
using veterinaria_yara_ux.infrastructure.mappings;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.infrastructure.data.repositories.login;
using veterinaria_yara_ux.infrastructure.data.repositories.Estados;
using veterinaria_yara_ux.infrastructure.data.repositories.Mascota;
using veterinaria_yara_ux.infrastructure.data.repositories.Raza;

namespace veterinaria_yara_ux.infrastructure.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loginBuilder => loginBuilder.AddSerilog(dispose: true));

            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<IEstados, EstadosRepository>();
            services.AddScoped<IMascota, MascotaRepository>();
            services.AddScoped<IRaza, RazaRepository>();

            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.Internal().MethodMappingEnabled = false;
            //    mc.AddProfile(new MappingProfile());
            //});

            //services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);


            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("RedisUrl") ?? "" },
                Password = configuration.GetConnectionString("RedisClave"),
                AbortOnConnectFail = false
            };

            services
                    .AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configurationOptions))
                    .BuildServiceProvider();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
