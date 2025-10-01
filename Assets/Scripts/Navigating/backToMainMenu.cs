using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMainMenu : MonoBehaviour
{
    // Build number of scene to start when start button is pressed
    [SerializeField] private string MainMenuScene = "MainMenu"; // set in Inspector

    public void StartMainMenu()
    {
        if (string.IsNullOrWhiteSpace(MainMenuScene))
        {
            Debug.LogError("Main Menu Scene name is empty.");
            return;
        }

        // Make sure the scene is added in Build Settings
        SceneManager.LoadScene(MainMenuScene);
    }
}
