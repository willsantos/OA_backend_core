using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
	public class AvaliacaoEntityToContractMap : Profile
	{
		public AvaliacaoEntityToContractMap()
		{
			CreateMap<Avaliacao, AvaliacaoRequest>().ReverseMap();
			CreateMap<Avaliacao, AvaliacaoResponse>().ReverseMap();
		}
	}
}
