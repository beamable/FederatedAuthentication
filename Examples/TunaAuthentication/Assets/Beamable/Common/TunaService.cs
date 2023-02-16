using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamable.Common
{
    public class TunaService
    {
        private static readonly Random Random = new();

        private static readonly IList<string> Tunas = new List<string>
        {
            "big blue tuna",
            "funky tuna",
            "yellow tuna"
        };

        public static Task<TunaResponse> GetUserByAuthorizationCode(string authorizationCode)
        {
            return Task.FromResult(new TunaResponse
            {
                userId = $"{Tunas[Random.Next(1, 3)]}-{authorizationCode}"
            });
        }

        public static string GetAuthorizationCode() => Guid.NewGuid().ToString();
    }

    public class TunaResponse
    {
        public string userId;
    }
}