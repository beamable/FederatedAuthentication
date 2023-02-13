using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Beamable.Common
{
    public class TunaService
    {
        public static Task<TunaResponse> GetUserByAuthorizationCode(string authorizationCode)
        {
            // We will just hash the auth code to get the external user id, for demo purposes.
            // You would interact with some external system in real-world scenarios.
            using var hash = SHA256.Create();
            byte[] bytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(authorizationCode));
            return Task.FromResult(new TunaResponse
            {
                userId = BitConverter.ToString(bytes).Replace("-", "").ToLower()
            });
        }

        public static string GetAuthorizationCode()
        {
            return "big-blue-tuna";
        }
    }
    
    public class TunaResponse
    {
        public string userId;
    }
}