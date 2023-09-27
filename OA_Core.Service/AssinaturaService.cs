using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Domain.Utils;


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
			
			var entity = _mapper.Map<Assinatura>(assinatura);

			if (await _usuarioRepository.ObterPorIdAsync(assinatura.UsuarioId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId {assinatura.UsuarioId} inválido ou não existente");

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return Guid.Empty;

			}

			var assinaturaExistente = await _assinaturaRepository.ObterAsync(a => a.UsuarioId == entity.UsuarioId);


			if (assinaturaExistente is not null)
			{				
				entity.DataAtivacao = assinaturaExistente.DataVencimento.AddDays(1);
				entity.DataVencimento = entity.DataCriacao.AddYears(1);
				await _assinaturaRepository.AdicionarAsync(entity);
				return entity.Id;
			}

			entity.DataAtivacao = DateTime.Now;
			entity.DataVencimento = entity.DataCriacao.AddYears(1);
			await _assinaturaRepository.AdicionarAsync(entity);
			return entity.Id;

		}

		public async Task<Guid> PutCancelarAssinaturaAsync(Guid id, AssinaturaCancelamentoRequest assinatura)
		{			
			var assinaturaParaCancelar = await _assinaturaRepository.ObterPorIdAsync(id);
			if(assinaturaParaCancelar is null) 
			{
				throw new InformacaoException(StatusException.NaoEncontrado, $"Assinatura {id} inválido ou não existe");
			}
			assinaturaParaCancelar.MotivoCancelamento = assinatura.MotivoCancelamento;
			assinaturaParaCancelar.DataCancelamento = DateTime.Now;
			assinaturaParaCancelar.Status = AssinaturaStatusEnum.Cancelada;
			await _assinaturaRepository.EditarAsync(assinaturaParaCancelar);

			return id;
		}
	}
}
