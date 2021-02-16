// Becca's script to navigate between scenes and quit game
// Adapted from Brackey's youtube tutorial: Start Menu in Unity 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // These manage the scene changes
    public void MicroscopeScene()
    {
        SceneManager.LoadScene("2-1_JuneAlmeidaMicroscopeScene");
    }

    public void HubworldScene()
    {
        SceneManager.LoadScene("HubWorld");
    }

    // This quits the game and includes a debug message when testing
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
