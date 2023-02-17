using System;
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

    private string _authorizationCode;

    async void Start()
    {
        var ctx = await BeamContext.Default.Instance;
        await ctx.Accounts.OnReady;

        _attachIdentityButton.onClick.AddListener(OnAttachClicked);
        _authorizeButton.onClick.AddListener(OnAuthorizeClicked);
        _listExternalIdentitiesButton.onClick.AddListener(OnListExternalIdentitiesClicked);
        _authorizationCode = Guid.NewGuid().ToString();
    }

    private async void OnAttachClicked()
    {
        var ctx = BeamContext.Default;

        Debug.Log("Attaching...");
        var response = await ctx.Accounts
            .AddExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(_authorizationCode);
        Debug.Log($"Is success: {response.isSuccess}");
    }

    private async void OnAuthorizeClicked()
    {
        var ctx = BeamContext.Default;
        if (!ctx.Accounts.Current.ExternalIdentities.Any())
        {
            Debug.Log("No external identity attached");
        }
        else
        {
            Debug.Log("Authorizing...");
            var accountRecoveryResponse = await ctx.Accounts
                .RecoverAccountWithExternalIdentity<TunaCloudIdentity, AuthenticationMicroserviceClient>(_authorizationCode);
            Debug.Log($"Is success: {accountRecoveryResponse.isSuccess}");
            await accountRecoveryResponse.SwitchToAccount();            
        }
    }

    private void OnListExternalIdentitiesClicked()
    {
        foreach (var external in BeamContext.Default.Accounts.Current.ExternalIdentities)
        {
            Debug.Log($"{external.providerService}/{external.providerNamespace} -> {external.userId}");
        }
    }
}