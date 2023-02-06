using Beamable;
using Beamable.Server;
using Beamable.Server.Clients;
using UnityEngine;
using UnityEngine.UI;

public class TunaScript : MonoBehaviour
{
    [SerializeField] private Button _attachIdentityButton;
    private AuthenticationMicroserviceClient _client;

    async void Start()
    {
        _attachIdentityButton.onClick.AddListener(OnAttachClicked);
        _client = new AuthenticationMicroserviceClient(BeamContext.Default);
    }

    private async void OnAttachClicked()
    {
        Debug.Log("Attaching...");
        var response = await _client.AttachIdentity("someTunaToken");
        Debug.Log($"Result: {response.result}");
    }
}