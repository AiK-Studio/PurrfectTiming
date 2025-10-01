using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructionButton : MonoBehaviour
{

    // Build number of scene to start when start button is pressed
    [SerializeField] private string InstructionsScene = "InstructionsScene"; // set in Inspector

    public void StartInstruction()
    {
        if (string.IsNullOrWhiteSpace(InstructionsScene))
        {
            Debug.LogError("Instructions scene name is empty.");
            return;
        }

        // Make sure the scene is added in Build Settings
        SceneManager.LoadScene(InstructionsScene);
    }
}
