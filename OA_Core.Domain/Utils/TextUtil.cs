using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Utils
{
    public static class TextUtil
    {
        public static int GetStatusCode(this StatusException status)
        {
            switch (status)
            {
                case StatusException.NaoAutorizado:
                    return 401;
                case StatusException.AcessoProibido:
                    return 403;
                case StatusException.NaoEncontrado:
                    return 404;
                case StatusException.NaoProcessado:
                    return 422;
				case StatusException.Conflito:
					return 409;
                default:
                    return 500;
            }
        }

        public static int? ToInt(this string value)
        {
            if (int.TryParse(value, out int result))
                return result;
            return null;
        }
    }
}
