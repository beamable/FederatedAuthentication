using System.Threading.Tasks;

namespace Beamable.Common
{
    public class TunaService
    {
        public static Task<TunaResponse> GetUserByAuthorizationCode(string authorizationCode)
        {
            return Task.FromResult(new TunaResponse
            {
                userId = $"big blue tuna-{authorizationCode}"
            });
        }
    }

    public class TunaResponse
    {
        public string userId;
    }
}