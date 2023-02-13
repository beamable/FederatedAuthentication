using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Beamable.Common
{
    public class TunaService
    {
        public static Task<TunaResponse> GetUserByAuthorizationCode(string authorizationCode)
        {
            // External user id has a corresponding tuna that matches the SHA256 hash of the input token  
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(authorizationCode));
                return Task.FromResult(new TunaResponse
                {
                    userId = BitConverter.ToString(bytes).Replace("-", "").ToLower()
                });
            }
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