﻿using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.ValueObjects;
using OA_Core.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Service
{
	[Collection(nameof(AlunoBogusCollection))]
	[Trait("Service", "Aluno Service")]
    public class AlunoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;
		private readonly AlunoFixture _alunoFixture;

        public AlunoServiceTest(AlunoFixture alunoFixture)
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _notifier = Substitute.For<INotificador>();
			_alunoFixture = alunoFixture;
        }

		[Fact(DisplayName = "Cadastra alunos")]
		public async Task AlunoService_CriaAluno_DeveCriar()
		{
			//Arrange
			var usuario = UsuarioFixture.GerarUsuario();
			var alunoRequest = _mapper.Map<AlunoRequest>(AlunoFixture.GerarAluno());
			var mockRepository = Substitute.For<IAlunoRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, mockUsuarioRepository, _mapper, _notifier);

			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);
			var resultado = await service.CadastrarAlunoAsync(alunoRequest);

			//Assert
			resultado.Should().NotBe(Guid.Empty, "Guid não pode ser nula");
		}

		[Theory(DisplayName = "Obtém todos os alunos")]
		[InlineData(1, 20)]
		[InlineData(1, 1)]
		[InlineData(1, 100)]
		[InlineData(3, 5)]
		[InlineData(2, 20)]
		[InlineData(4, 5)]
		public async Task AlunoService_ObtemAluno_DeveRetornarLista(int pagina, int linhas)
        {
			//Arrange
            var alunos = AlunoFixture.GerarAlunos(linhas, true);
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			mockRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(alunos);
			var resultado = await service.ObterTodosAlunosAsync(pagina, linhas);

			//Assert
			resultado.Should().HaveCount(linhas);
		}

        [Fact(DisplayName = "Obtém alunos por id")]
        public async Task AlunoService_ObtemAluno_DeveRetornarUmAluno()
        {
			//Arrange
			var aluno = AlunoFixture.GerarAluno();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);
			var alunoResponse = _mapper.Map<AlunoResponse>(aluno);

			//Act
			mockRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aluno);
            var result = await service.ObterAlunoPorIdAsync(aluno.Id);

			//Assert
			result.Should().BeEquivalentTo(alunoResponse);
        }

        [Fact(DisplayName = "Tenta obter alunos por id inválido")]
        public async Task AlunoService_ObtemAlunoNull_DeveSerInvalido()
        {
			//Arrange
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => service.ObterAlunoPorIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta aluno")]
        public async Task AlunoService_DeletaAluno_DeveDeletar()
        {
			//Arrange
            var aluno = _fixture.Create<Aluno>();
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			mockRepository.ObterPorIdAsync(aluno.Id).Returns(aluno);
			await service.DeletarAlunoAsync(aluno.Id);

			//Assert
            await mockRepository.Received().EditarAsync(aluno);
        }

		[Fact(DisplayName = "Tenta cadastrar aluno com foto inválida")]
		public async Task AlunoService_CadastraAlunoFotoInvalida_DeveSerInvalido()
		{
			//Arrange
			var usuario = _fixture.Create<Usuario>();
			var alunoRequest = _fixture.Create<AlunoRequest>();
			alunoRequest.Foto = string.Empty;
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);
			await service.CadastrarAlunoAsync(alunoRequest);

			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Foto" && e.ErrorMessage == "É necessário anexar a foto")));
		}

		[Fact(DisplayName = "Cadastra aluno com usuarioId inválido")]
		public async Task AlunoService_CadastraAlunoUsuarioIdInvalido_DeveSerInvalido()
		{
			//Arrange
			var alunoRequest = _fixture.Create<AlunoRequest>();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act

			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => service.CadastrarAlunoAsync(alunoRequest));
		}

		[Fact(DisplayName = "Atualiza um aluno")]
		public async Task AlunoService_AtualizaAluno_DeveAtualizar()
		{
			//Arrange
			var aluno = _fixture.Create<Aluno>();
			var alunoRequest = _fixture.Create<AlunoRequestPut>();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			mockRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aluno);
			await service.EditarAlunoAsync(aluno.Id, alunoRequest);

			//Assert
			await mockRepository.Received().EditarAsync(Arg.Is<Aluno>(x => x.Foto == alunoRequest.Foto));
		}

		[Fact(DisplayName = "Atualiza aluno com foto inválida")]
		public async Task AlunoService_AtualizaAlunoFotoInvalida_DeveAtualizar()
		{
			//Arrange
			var aluno = _fixture.Create<Aluno>();
			var alunoRequest = _fixture.Create<AlunoRequestPut>();
			alunoRequest.Foto = string.Empty;
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			//Act
			mockRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aluno);
			await service.EditarAlunoAsync(aluno.Id, alunoRequest);

			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Foto" && e.ErrorMessage == "É necessário anexar a foto")));
		}
	}
}
