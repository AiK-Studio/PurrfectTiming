using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.Services.Multiplayer;
using System.Threading.Tasks;
using System;
using System.Linq;

public class SessionManager : MonoBehaviour
{
    public static SessionManager I;

    public ISession CurrentSession { get; private set; }
    public event Action<int> OnPlayerCountChanged;
    [SerializeField] private string gameSceneName = "SampleScene";

    void Awake()
    {
        //If it is being created another version then it destroy itself
        if (I != null) { Destroy(gameObject); return; }
        I = this; DontDestroyOnLoad(gameObject);
    }

    // HOST: create session (will add real code next), then start host
    public async Task<string> CreateAndHostAsync(string sessionName, int maxPlayers)
    {
        try
        {
            var options = new SessionOptions { MaxPlayers = maxPlayers }.WithRelayNetwork();
            CurrentSession = await MultiplayerService.Instance.CreateSessionAsync(options);


            WireSessionEvents(CurrentSession);
            RaiseCount(); // initial count  

            // Return the code so UI can show it
            return CurrentSession.Code;
        }
        catch (Exception ex)
        {
            Debug.LogError("Create host failed: " + ex.Message);
            return null;
        }
    }

    // CLIENT: join by code (will add real code next), then start client
    public async Task JoinAsClientAsync(string joinCode)
    {
        try
        {
            CurrentSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(joinCode);
            WireSessionEvents(CurrentSession);
            RaiseCount(); // initial
        }
        catch (Exception ex)
        {
            Debug.LogError("Join failed: " + ex.Message);
        }


    }

    // Host leaves or client wants to exit back to menu
    public void ShutdownToMenu()
    {
        if (NetworkManager.Singleton.IsListening)
            NetworkManager.Singleton.Shutdown();

        SceneManager.LoadScene("MainMenu");
    }

    // Host-only: start the match → sync-load game scene for everyone
    public void StartMatch()
    {
        if (NetworkManager.Singleton == null) { Debug.LogError("No NetworkManager."); return; }
        //Only the host allow to start the match
        if (!NetworkManager.Singleton.IsHost)
        {
            Debug.LogWarning("Only the host can start the match.");
            return;
        }


        // Use NGO's SceneManager so clients load with the host
        NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
    void WireSessionEvents(ISession s)
    {
        s.PlayerJoined += _ => RaiseCount();
        s.PlayerLeaving += _ => RaiseCount();
        s.Changed += () => RaiseCount();
    }

    void RaiseCount()
    {
        try
        {
            //If Count on the left is null then use 1 as the count
            int count = CurrentSession?.Players?.Count() ?? 1;
            OnPlayerCountChanged?.Invoke(count);
        }
        catch
        {
            OnPlayerCountChanged?.Invoke(1);
        }
    }
}
