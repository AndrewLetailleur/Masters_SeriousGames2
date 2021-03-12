// Becca's script to navigate between scenes and quit game
// Adapted from Brackey's youtube tutorial: Start Menu in Unity 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManager gm;
//    Scene scene;//string var blah

    //Awake is called first, when the script instance is being loaded
    void Awake() { gm = FindObjectOfType<GameManager>(); }

    //Andrew L Edit = a bit inefficient? So instead, going to try and make a "generic name X" Scene manager, in case it 'just works'.
    public void GenericScene(string scene_name) { SceneManager.LoadScene(scene_name); }
    //not needed, as GameManager does this script entirely
    ///public void HubworldScene() { SceneManager.LoadScene("HubWorld"); }
    ///public void MicroscopeScene()  { SceneManager.LoadScene("2-1_JuneAlmeidaMicroscopeScene"); }

    //sets gm bools to true, if calling back as a victory condition. Before checking with gm/GameManager script
    public void HubworldReturn(int val)
    {
        switch (val)
        {
            case 1:
                gm.First = true;
                break;
            case 2:
                gm.Second = true;
                break;
            case 3:
                gm.Third = true;
                break;
            default:
                break;
        }
        gm.HubworldScene();// SceneManager.LoadScene("HubWorld");
    }

    // This quits the game and includes a debug message when testing
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
