using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;


namespace OA_Core.Service
{
	public class AssinaturaService : IAssinaturaService
	{
		private readonly IMapper _mapper;
		private readonly IAssinaturaRepository _assinaturaRepository;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly INotificador _notificador;

		public AssinaturaService(IMapper mapper, IAssinaturaRepository assinaturaRepository, 
							     IUsuarioRepository usuarioRepository, INotificador notificador)
		{
			_usuarioRepository = usuarioRepository;
			_mapper = mapper;
			_assinaturaRepository = assinaturaRepository;
			_notificador = notificador;
		}

		public async Task<Guid> PostAssinaturaAsync(AssinaturaRequest assinatura)
		{
			//O registro de uma assinatura já inclui a data de ativação e de vencimento.

			
			var entity = _mapper.Map<Assinatura>(assinatura);

			if (await _usuarioRepository.ObterPorIdAsync(assinatura.UsuarioId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId {assinatura.UsuarioId} inválido ou não existente");

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return Guid.Empty;

			}

			var assinaturaExistente = await _assinaturaRepository.BuscarAssinaturaPorUsuarioId(entity.UsuarioId);


			if (assinaturaExistente is not null)
			{
				entity.DataCriacao = assinaturaExistente.DataVencimento.AddDays(1);
				entity.DataVencimento = entity.DataCriacao.AddYears(1);
				await _assinaturaRepository.CadastrarAssinaturaAsync(entity);
				return entity.Id;
			}

			entity.DataAtivacao = DateTime.Now;
			entity.DataVencimento = entity.DataCriacao.AddYears(1);
			await _assinaturaRepository.CadastrarAssinaturaAsync(entity);
			return entity.Id;

		}

		public Task<Guid> PutCancelarAssinaturaAsync(AssinaturaCancelamentoRequest assinatura)
		{
			throw new NotImplementedException();
		}
	}
}
