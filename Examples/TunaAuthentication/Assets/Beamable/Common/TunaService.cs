using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beamable.Common
{
    public class TunaService
    {
        private static readonly Random Random = new Random();
        private static readonly IDictionary<int, string> Tunas = new Dictionary<int, string>()
        {
            { 1, "big blue tuna" },
            { 2, "funky tuna" },
            { 3, "yellow tuna" }
        }; 
    
        public static Task<TunaResponse> GetUserByAuthorizationCode(string authorizationCode)
        {
            return Task.FromResult(new TunaResponse
            {
                userId = Tunas[Convert.ToInt32(authorizationCode)]
            });
        }

        public static string GetAuthorizationCode() => Random.Next(1, 3).ToString();
    }
    
    public class TunaResponse
    {
        public string userId;
    }
}