using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class StartButtonGate : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private int minPlayersToStart = 2;
    int currentCount = 1;

    void OnEnable()
    {
        Refresh();
        if (SessionManager.I != null)
            SessionManager.I.OnPlayerCountChanged += OnCount;

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted += Refresh;
            NetworkManager.Singleton.OnClientConnectedCallback += _ => Refresh();
            NetworkManager.Singleton.OnClientDisconnectCallback += _ => Refresh();
        }
    }

    void OnDisable()
    {
        if (SessionManager.I != null)
            SessionManager.I.OnPlayerCountChanged -= OnCount;

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= Refresh;
            NetworkManager.Singleton.OnClientConnectedCallback -= _ => Refresh();
            NetworkManager.Singleton.OnClientDisconnectCallback -= _ => Refresh();
        }
    }

    void OnCount(int count) { currentCount = count; Refresh(); }

    void Refresh()
    {
        bool isHost = NetworkManager.Singleton && NetworkManager.Singleton.IsHost;
        bool canStart = isHost && currentCount >= minPlayersToStart;

        // hide for clients OR show/disable — pick ONE line:
        // startButton.gameObject.SetActive(canStart);       // hide until host + min players
        if (startButton) startButton.interactable = canStart; // or: keep visible, gray it out
    }

    public void OnStartClicked()
    {
        if (NetworkManager.Singleton && NetworkManager.Singleton.IsHost)
            SessionManager.I.StartMatch();
    }
}
