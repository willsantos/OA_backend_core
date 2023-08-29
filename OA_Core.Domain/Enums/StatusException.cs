using System.ComponentModel;

namespace OA_Core.Domain.Enums
{
    public enum StatusException
    {
        [Description("Nenhum")]
        Nenhum,
        [Description("Ocorreu algo inesperado")]
        Erro,
        [Description("Dado não encontrado")]
        NaoEncontrado,
        [Description("Campo(s) com formato(s) incorreto(s)")]
        FormatoIncorreto,
        [Description("Acesso não autorizado")]
        NaoAutorizado,
        [Description("Dado não processado")]
        NaoProcessado,
        [Description("Acesso proibido")]
        AcessoProibido
    }
}
