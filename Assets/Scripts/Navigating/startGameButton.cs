using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Build number of scene to start when start button is pressed
    [SerializeField] private string GameScene = "SampleScene"; // set in Inspector

    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(GameScene))
        {
            Debug.LogError("Game Scene name is empty.");
            return;
        }

        // Make sure the scene is added in Build Settings
        SceneManager.LoadScene(GameScene);
    }
}