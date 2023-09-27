using OA_Core.Domain.Enums;
using OA_Core.Domain.Utils;
using System.Text.Json.Serialization;

namespace OA_Core.Domain.Contracts.Response
{
    public class InformacaoResponse
    {
        public StatusException Codigo { get; set; }
        public string Descricao { get { return Codigo.Description(); } }
        public List<string>? Mensagens { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? MensagemDebug { get; set; }
		
    }
}
