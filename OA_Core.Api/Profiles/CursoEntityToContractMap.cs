using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Api.Profiles
{
    public class CursoEntityToContractMap : Profile
    {
        public CursoEntityToContractMap()
        {
            CreateMap<Curso, CursoRequest>().ReverseMap();
            CreateMap<Curso, CursoResponse>().ReverseMap();
            CreateMap<Curso, CursoRequestPut>().ReverseMap();
			CreateMap<Curso, CursoParaUsuarioResponse>().ReverseMap();

        }
    }
}
