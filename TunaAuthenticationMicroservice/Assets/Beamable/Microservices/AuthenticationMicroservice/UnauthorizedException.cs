using System.Net;
using Beamable.Server;

namespace Beamable.Microservices.AuthenticationMicroservice
{
    internal class UnauthorizedException : MicroserviceException

    {
        public UnauthorizedException() : base((int)HttpStatusCode.Unauthorized, "Unauthorized", "")
        {
        }
    }
}