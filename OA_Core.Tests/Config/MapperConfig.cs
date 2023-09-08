using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Config
{
    internal class MapperConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Api.Profiles.CursoEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.ProfessorEntityToContractMap());
                cfg.AddProfile(new Api.Profiles.UsuarioEntityToContractMap());
            });

            return mockMapper.CreateMapper();
        }
    }
}
