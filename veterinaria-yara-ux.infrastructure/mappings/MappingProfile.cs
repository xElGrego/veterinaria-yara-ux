using AutoMapper;
using veterinaria_yara_ux.domain.DTOs;
using veterinaria_yara_ux.domain.DTOs.Estados;
using veterinaria_yara_ux.domain.DTOs.Raza;
using veterinaria_yara_ux.domain.DTOs.Usuario;
 
namespace veterinaria_yara_ux.infrastructure.mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<Mascota, MascotaDTO>().ReverseMap();



            //CreateMap<AgregarUsuarioDTO, Usuario>()
            //.ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now))
            //.ForMember(x => x.Estado, d => d.MapFrom(model => 2));



            //CreateMap<NuevaMascotaDto, Mascota>()
            //  .ForMember(x => x.IdMascota, d => d.MapFrom(model => Guid.NewGuid()))
            //  .ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now))
            //  .ForMember(x => x.Estado, d => d.MapFrom(model => 2));


            //CreateMap<Raza, RazaDTO>().ReverseMap();

            //CreateMap<NuevaRazaDTO, Raza>()
            //.ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now));


            //CreateMap<NuevoUsuarioDTO, Usuario>()
            //.ForMember(x => x.IdUsuario, d => d.MapFrom(model => Guid.NewGuid()))
            //.ForMember(x => x.FechaIngreso, d => d.MapFrom(model => DateTime.Now))
            //.ForMember(x => x.Clave, d => d.MapFrom(model => model.Clave))
            //.ForMember(x => x.Estado, d => d.MapFrom(model => true));

            //CreateMap<Usuario, UsuarioDTO>().ReverseMap();


            //CreateMap<MensajeDTO, Mensaje>();

            //CreateMap<Estado, EstadosDTO>().ReverseMap();

        }
    }
}
