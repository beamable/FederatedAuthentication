using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Beamable.Microservices.AuthenticationMicroservice
{
    internal static class TunaService
    {
        public static Task<TunaResponse> GetUserByAuthorizationCode(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedException();
            }

            // External user id has a corresponding tuna that matches the SHA256 hash of the input token  
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
                return Task.FromResult(new TunaResponse
                {
                    userId = BitConverter.ToString(bytes).Replace("-", "").ToLower()
                });
            }
        }
    }

    internal class TunaResponse
    {
        public string userId;
    }
}