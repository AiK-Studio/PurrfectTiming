using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewDemoButton : MonoBehaviour
{
    // Build number of scene to start when start button is pressed
    [SerializeField] private string DemoScene = "DemoScene"; // set in Inspector

    public void ViewDemo()
    {
        if (string.IsNullOrWhiteSpace(DemoScene))
        {
            Debug.LogError("Demo Scene name is empty.");
            return;
        }

        // Make sure the scene is added in Build Settings
        SceneManager.LoadScene(DemoScene);
    }
}