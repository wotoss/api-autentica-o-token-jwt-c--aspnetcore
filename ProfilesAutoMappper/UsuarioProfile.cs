using AutoMapper;
using UsuariosApi.Data.Dtos;
using UsuariosApi.ModeloEntidade;

namespace UsuariosApi.ProfilesAutoMappper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            //esterei passando (requisição) o que vem do usuario client
            //para a persistencisia dos ( dados ) usuarios
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
