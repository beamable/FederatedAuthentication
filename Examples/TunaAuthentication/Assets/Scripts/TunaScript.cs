using Beamable;
using Beamable.Server;
using Beamable.Server.Clients;
using UnityEngine;
using UnityEngine.UI;

public class TunaScript : MonoBehaviour
{
    [SerializeField] private Button _attachIdentityButton;
    [SerializeField] private Button _authorizeButton;
    private AuthenticationMicroserviceClient _client;

    void Start()
    {
        _attachIdentityButton.onClick.AddListener(OnAttachClicked);
        _authorizeButton.onClick.AddListener(OnAuthorizeClicked);
        _client = new AuthenticationMicroserviceClient(BeamContext.Default);
    }

    private async void OnAttachClicked()
    {
        Debug.Log("Attaching...");
        var response = await _client.AttachIdentity("someTunaToken");
        Debug.Log($"Result: {response.result}");
    }
    
    private async void OnAuthorizeClicked()
    {
        Debug.Log("Authorizing...");
        var response = await _client.AuthorizeExternalIdentity("someTunaToken");
        Debug.Log($"External user id: {response.user_id}");
    }
}