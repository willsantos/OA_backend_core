using OA_Core.Domain.Enums;
using OA_Core.Domain.Utils;

namespace OA_Core.Domain.Contracts.Response
{
    public class InformacaoResponse
    {
        public StatusException Codigo { get; set; }
        public string Descricao { get { return Codigo.Description(); } }
        public List<string> Mensagens { get; set; }
    }
}
