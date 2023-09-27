using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
	public class AssinaturaEntityToContractMap : Profile
	{
		public AssinaturaEntityToContractMap()
		{
			CreateMap<Assinatura, AssinaturaRequest>().ReverseMap();
			CreateMap<Assinatura, AssinaturaCancelamentoRequest>().ReverseMap();
			CreateMap<Assinatura, AssinaturaResponse>().ReverseMap();
		}
	}
}
