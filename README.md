# FederatedAuthentication
Beamable supports custom authentication federation using managed [microservices](https://docs.beamable.com/docs/microservices-feature-overview). You can use this feature to implement an OAuth2, OpenID Connect or a custom external authententication provider and use it with Beamable. We also support two-way chellenge-based flows for PKI based authentication for Web3/blockchain scenarios.  
  
Some use cases:
- Blockchain wallet authentication - Attach a wallet to a players account and use it for authentication.
- External authentication provider integration - Already using something like Auth0? Use it for your game to achive a Single Sign-On experience.

# Steps

## 1. Create a microservice
You should first create a microservice or use an existing one.  
*NOTE: You can bundle multiple authentication federations in a single microservice using namespaces (more on this below).*

## 2. Implement an IThirdPartyCloudIdentity interface
Create an implementation of *IThirdPartyCloudIdentity* in **Beamable/Common** (or any shared assembly).  
This will create a "tuna" namespace for your new authentication scheme. Namespace will be a part of the authentication endpoint path.   
E.g. */your_microservice/tuna/atuhenticate*

```csharp
public class TunaCloudIdentity : IThirdPartyCloudIdentity
{
	public string UniqueName => "tuna";
}
```

## 3. Implement IFederatedLogin<TunaCloudIdentity> in your microservice
Let's say you already have a TunaService that holds all your user's data. We will use it to validate a "token" received by the client and respond with a "user_id".
```csharp
[Microservice("AuthenticationMicroservice")]
public class AuthenticationMicroservice : IFederatedLogin<TunaCloudIdentity>
{
  public async Promise<FederatedAuthenticationResponse> Authenticate(string token, string challenge, string solution)
  {
    // Token can be something like an authorization_code, depending on your client and service implementations
    var tunaUserResponse = await TunaService.GetUserByAuthorizationCode(token);
    if (tunaUserResponse == null)
    {
        throw new UnauthorizedException();
    }
    return new FederatedAuthenticationResponse { user_id = tunaUserResponse.userId };
  }
}
```
In this example we didn't use the "challenge" and "solution" arguments. Standard use-case for challenges is wallet authentication. If a client sends us a wallet address as a token, the only way to verify the ownership of that wallet is to issue a challenge, and require a user to sign that challege using his private key. [Solana/Phantom wallet authentication](https://github.com/beamable/solana-example) is an example that uses a challenge.

## 4. Publich your microservice

## 5. CLIENT: Attach an external identity to a player
```csharp
var ctx = BeamContext.Default;
var tunaAuthCode = await TunaService.GetAuthorizationCode();
var response = await ctx.Accounts
  .AddExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(tunaAuthCode);
```

## 6. CLIENT: Login using an external identity
```csharp
var ctx = BeamContext.Default;
var tunaAuthCode = await TunaService.GetAuthorizationCode();
var accountRecoveryResponse = await ctx.Accounts
  .RecoverAccountWithExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(tunaAuthCode);
await accountRecoveryResponse.SwitchToAccount();
```

# Beamable provided examples
- [Solana/Phantom authentication and inventory federation](https://github.com/beamable/solana-example)