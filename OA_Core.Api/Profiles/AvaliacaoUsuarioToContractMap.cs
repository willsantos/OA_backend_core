using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
	public class AvaliacaoUsuarioToContractMap : Profile
	{
		public AvaliacaoUsuarioToContractMap()
		{
			CreateMap<AvaliacaoUsuario, AvaliacaoUsuarioRequest>().ReverseMap();
			CreateMap<AvaliacaoUsuario, AvaliacaoUsuarioResponse>().ReverseMap();
		}
	}
}
