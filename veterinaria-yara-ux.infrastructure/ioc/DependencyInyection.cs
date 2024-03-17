using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
using veterinaria_yara_ux.infrastructure.mappings;
using veterinaria_yara_ux.application.interfaces.repositories;
using veterinaria_yara_ux.infrastructure.data.repositories.Estados;
using veterinaria_yara_ux.infrastructure.data.repositories.Mascota;
using veterinaria_yara_ux.infrastructure.data.repositories.Raza;
using veterinaria_yara_ux.infrastructure.data.repositories.Usuario;
using MassTransit;
using RabbitMQ.Client;
using veterinaria_yara_ux.domain.DTOs.RabbitMQ;
using veterinaria_yara_ux.infrastructure.data.repositories.Notificacion;

namespace veterinaria_yara_ux.infrastructure.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loginBuilder => loginBuilder.AddSerilog(dispose: true));

            services.AddScoped<IUsuario, UsuarioRepository>();
            services.AddScoped<IEstados, EstadosRepository>();
            services.AddScoped<IMascota, MascotaRepository>();
            services.AddScoped<IRaza, RazaRepository>();
            services.AddScoped<INotificaciones, NotificacionRepository>();

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

            //services.AddMassTransit(mt =>
            //{
            //    mt.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host(new Uri("rabbitmq://localhost"), host =>
            //        {
            //            host.Username("grego977");
            //            host.Password("yara19975");
            //        });

            //        cfg.Publish<UserCreated>(x =>
            //        {
            //            x.ExchangeType = ExchangeType.Topic;
            //        });

            //        cfg.Send<UserCreated>(x =>
            //        {
            //            x.UseRoutingKeyFormatter(context => "queue");
            //        });
            //    });
            //});

            return services;
        }
    }
}
