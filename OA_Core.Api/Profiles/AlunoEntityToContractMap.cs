using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class AlunoEntityToContractMap : Profile
    {
        public AlunoEntityToContractMap()
        {
            CreateMap<Aluno, AlunoRequest>().ReverseMap();
            CreateMap<Aluno, AlunoResponse>().ReverseMap();
            CreateMap<Aluno, AlunoRequestPut>().ReverseMap();
        }
    }
}
