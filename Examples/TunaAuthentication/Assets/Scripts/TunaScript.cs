using System.Linq;
using Beamable;
using Beamable.Common;
using Beamable.Server.Clients;
using UnityEngine;
using UnityEngine.UI;

public class TunaScript : MonoBehaviour
{
    [SerializeField] private Button _attachIdentityButton;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _listExternalIdentitiesButton;

    async void Start()
    {
        var ctx = BeamContext.Default;

        await ctx.OnReady;
        await ctx.Accounts.OnReady;

        _attachIdentityButton.onClick.AddListener(OnAttachClicked);
        _authorizeButton.onClick.AddListener(OnAuthorizeClicked);
        _listExternalIdentitiesButton.onClick.AddListener(OnListExternalIdentitiesClicked);
    }

    private async void OnAttachClicked()
    {
        var ctx = BeamContext.Default;

        Debug.Log("Attaching...");
        var tunaAuthCode = TunaService.GetAuthorizationCode();
        var response = await ctx.Accounts
            .AddExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(tunaAuthCode);
        Debug.Log($"Is success: {response.isSuccess}, Tuna ID: {response.account.ExternalIdentities.First().userId}");
    }

    private async void OnAuthorizeClicked()
    {
        var ctx = BeamContext.Default;

        Debug.Log("Authorizing...");
        var tunaAuthCode = TunaService.GetAuthorizationCode();
        var response = await ctx.Accounts
            .RecoverAccountWithExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(tunaAuthCode);
        Debug.Log($"Is success: {response.isSuccess}");
    }

    private void OnListExternalIdentitiesClicked()
    {
        foreach (var external in BeamContext.Default.Accounts.Current.ExternalIdentities)
        {
            Debug.Log($"{external.providerService}/{external.providerNamespace} -> {external.userId}");
        }
    }
}