using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class CursoProfessorEntityToContractMap : Profile
    {
        public CursoProfessorEntityToContractMap()
        {
			CreateMap<CursoProfessor, CursoProfessorRequest>().ReverseMap();
			//CreateMap<CursoProfessor, CursoProfessorResponse>().ReverseMap();
		}
	}
}
