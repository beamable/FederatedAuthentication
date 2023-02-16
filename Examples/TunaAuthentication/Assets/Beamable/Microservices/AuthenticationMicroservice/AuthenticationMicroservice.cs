using Beamable.Common;
using Beamable.Server;

namespace Beamable.Microservices.AuthenticationMicroservice
{
    [Microservice("AuthenticationMicroservice")]
    public class AuthenticationMicroservice : Microservice, IFederatedLogin<TunaCloudIdentity>
    {
        public async Promise<FederatedAuthenticationResponse> Authenticate(string token, string challenge,
            string solution)
        {
            // Token can be something like an authorization_code, depending on your client and service implementations
            var tunaUserResponse = await TunaService.GetUserByAuthorizationCode(token);
            if (tunaUserResponse == null)
            {
                throw new UnauthorizedException();
            }

            BeamableLogger.Log("User ID: {tuna}", tunaUserResponse.userId);
            return new FederatedAuthenticationResponse { user_id = tunaUserResponse.userId };
        }
    }
}