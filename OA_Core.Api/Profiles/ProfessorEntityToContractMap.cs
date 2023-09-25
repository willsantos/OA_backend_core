using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class ProfessorEntityToContractMap : Profile
    {
        public ProfessorEntityToContractMap()
        {
            CreateMap<Professor, ProfessorRequest>().ReverseMap();
            CreateMap<Professor, ProfessorResponse>().ReverseMap();
			CreateMap<Professor, ProfessorResponseComResponsavel>().ReverseMap();
			CreateMap<Professor, ProfessorRequestPut>().ReverseMap();
        }
    }
}
