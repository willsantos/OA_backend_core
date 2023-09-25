using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.ValueObjects;
using OA_Core.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Service
{
    [Trait("Service", "Aluno Service")]
    public class AlunoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;

        public AlunoServiceTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _notifier = Substitute.For<INotificador>();
        }

        [Fact(DisplayName = "Obter todos os alunos")]
        public async Task ObterTodos()
        {
            var alunos = _fixture.Create<List<Aluno>>();
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

            var page = 1;
            var rows = 10;

            mockRepository.ObterTodosAsync(page, rows).Returns(alunos);

            var result = await service.GetAllAlunosAsync(page, rows);

            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Obter alunos por id")]
        public async Task ObterPorId()
        {
            var aluno = _fixture.Create<Aluno>();
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.ObterPorIdAsync(aluno.Id).Returns(aluno);

            var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

            var result = await service.GetAlunoByIdAsync(aluno.Id);

            Assert.Equal(result.Id, aluno.Id);
        }

        [Fact(DisplayName = "Obter alunos por id nulo")]
        public async Task ObterPorIdNull()
        {
            var aluno = _fixture.Create<Aluno>();
            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.ObterPorIdAsync(aluno.Id).Returns((Aluno)null);

            var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

            var exception = await Record.ExceptionAsync(async () => await service.GetAlunoByIdAsync(aluno.Id));
            Assert.NotNull(exception);
        }

        [Fact(DisplayName = "Cadastra alunos")]
        public async Task CadastrarAluno()
        {
			var usuario = _fixture.Create<Usuario>();
            var alunoRequest = _fixture.Create<AlunoRequest>();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

            var exception = await Record.ExceptionAsync(async () => await service.PostAlunoAsync(alunoRequest));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Deleta alunos")]
        public async Task DeletarAluno()
        {
            var aluno = _fixture.Create<Aluno>();

            var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.ObterPorIdAsync(aluno.Id).Returns(aluno);
            await mockRepository.RemoverAsync(aluno);

            var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

            await service.DeleteAlunoAsync(aluno.Id);

            var exception = await Record.ExceptionAsync(async () => await service.DeleteAlunoAsync(aluno.Id));
            Assert.Null(exception);
        }

		[Fact(DisplayName = "Cadastra aluno com cpf vazio")]
		public async Task CadastrarAlunoCpfVazio()
		{
			var usuario = _fixture.Create<Usuario>();
			var alunoRequest = _fixture.Create<AlunoRequest>();
			var cpfVazio = new Cpf(string.Empty);
			alunoRequest.Cpf = cpfVazio;
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			var exception = await Record.ExceptionAsync(async () => await service.PostAlunoAsync(alunoRequest));
			Assert.NotNull(exception);
		}

		[Fact(DisplayName = "Cadastra aluno com foto vazia")]
		public async Task CadastrarAlunoFotoVazia()
		{
			var usuario = _fixture.Create<Usuario>();
			var alunoRequest = _fixture.Create<AlunoRequest>();
			alunoRequest.Foto = string.Empty;
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			await service.PostAlunoAsync(alunoRequest);
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Foto" && e.ErrorMessage == "É necessário anexar a foto")));
		}

		[Fact(DisplayName = "Cadastra aluno com usuarioId inválido")]
		public async Task CadastrarAlunoUsuarioIdInvalido()
		{
			var alunoRequest = _fixture.Create<AlunoRequest>();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.AdicionarAsync(Arg.Any<Aluno>()).Returns(Task.CompletedTask);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			var exception = await Record.ExceptionAsync(async () => await service.PostAlunoAsync(alunoRequest));
			Assert.NotNull(exception);
		}

		[Fact(DisplayName = "Edita alunos")]
		public async Task EditarAluno()
		{
			var aluno = _fixture.Create<Aluno>();
			var alunoRequest = _fixture.Create<AlunoRequestPut>();
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aluno);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);

			var exception = await Record.ExceptionAsync(async () => await service.PutAlunoAsync(aluno.Id, alunoRequest));
			Assert.Null(exception);
		}

		[Fact(DisplayName = "Edita alunos campo inválido")]
		public async Task EditarAlunoNull()
		{
			var aluno = _fixture.Create<Aluno>();
			var alunoRequest = _fixture.Create<AlunoRequestPut>();
			alunoRequest.Foto = string.Empty;
			var mockRepository = Substitute.For<IAlunoRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();

			mockRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aluno);

			var service = new AlunoService(mockRepository, MockUsuarioRepository, _mapper, _notifier);
			await service.PutAlunoAsync(aluno.Id, alunoRequest);

			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Foto" && e.ErrorMessage == "É necessário anexar a foto")));
		}
	}
}
