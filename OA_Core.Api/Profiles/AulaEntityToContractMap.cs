using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class AulaEntityToContractMap : Profile
    {
        public AulaEntityToContractMap()
        {
            CreateMap<Aula, AulaRequest>().ReverseMap();
			CreateMap<Aula, AulaResponse>().ReverseMap();
            CreateMap<Aula, AulaRequestPut>().ReverseMap();

			CreateMap<AulaOnline, AulaRequest>().ReverseMap();
			CreateMap<AulaVideo, AulaRequest>().ReverseMap();
			CreateMap<AulaTexto, AulaRequest>().ReverseMap();
			CreateMap<AulaDownload, AulaRequest>().ReverseMap();

			CreateMap<AulaOnline, Aula>().ReverseMap();
			CreateMap<AulaVideo, Aula>().ReverseMap();
			CreateMap<AulaTexto, Aula>().ReverseMap();
			CreateMap<AulaDownload, Aula>().ReverseMap();
		}
    }
}
