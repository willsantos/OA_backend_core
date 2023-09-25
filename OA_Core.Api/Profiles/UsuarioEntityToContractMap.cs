using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class UsuarioEntityToContractMap : Profile
    {
        public UsuarioEntityToContractMap()
        {            
			CreateMap<Usuario, UsuarioRequest>().ReverseMap();
			CreateMap<Usuario, UsuarioResponse>().ReverseMap();
		}
    }
}
