using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Response
{
    public class QuestaoResponse : BaseResponse
    {
        public string Enuciado { get; set; }
        public string Alternativa_A { get; set; }
        public string Alternativa_B { get; set; }
        public string Alternativa_C { get; set; }
        public string Alternativa_D { get; set; }
        public string Alternativa_E { get; set; }
        public RespostaQuestao Resposta { get; set; }
        public int Dificuldade { get; set; }
        public Guid AvaliacaoId { get; set; }
    }
}
