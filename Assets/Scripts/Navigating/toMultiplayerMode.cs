using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMultiplayerMode : MonoBehaviour
{
    [SerializeField] private string multiplayerSceneName = "MultiplayerScene"; // set in Inspector

    public void GoToMultiplayer()
    {
        if (string.IsNullOrWhiteSpace(multiplayerSceneName))
        {
            Debug.LogError("Multiplayer scene name is empty.");
            return;
        }

        // Make sure the scene is added in Build Settings
        SceneManager.LoadScene(multiplayerSceneName);
    }
}
