using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Config
{
    public static class MapperConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Api.Profiles.CursoEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.ProfessorEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.UsuarioEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.AulaEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.AlunoEntityToContractMap());
				cfg.AddProfile(new Api.Profiles.CursoProfessorEntityToContractMap());
				cfg.AddProfile(new Api.Profiles.AssinaturaEntityToContractMap());
				cfg.AddProfile(new Api.Profiles.UsuarioCursoEntityToContractMap());
				cfg.AddProfile(new Api.Profiles.AvaliacaoEntityToContractMap());
				cfg.AddProfile(new Api.Profiles.AvaliacaoUsuarioToContractMap());

			});

            return mockMapper.CreateMapper();
        }
    }
}
