using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**************************************************
*    Title: PortraitInteraction
*    Author: Danica
*    >Edited/tweaked by, Andrew Letailleur
*    Date: 10/03/2021
*    Code version: 1.1 (Tweak)
*    Availability: Here
*    ===============
*    References: switch case logic, set up wise
**************************************************/

/// <summary>
/// The purpose of this script, is [BLAH]
/// =
/// PS: Comment this code, if you wrote it!
/// </summary>
public class PortraitInteraction : MonoBehaviour
{
    GameManager gm;

    //checks for 'easy' set up of scene jumping, three mini-game wise
    public enum Stage {Puzzle, Rocket, Virus};
    public Stage stage;

    //Awake is called first, when the script instance is being loaded
    void Awake() 
    {
        gm = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Player")
        {
            //added cursor debug code here, on scene transitions. = AIL
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log("Player touched portrait");

            //scene switch statement
            switch (stage)
            {
                case Stage.Puzzle:
                    SceneManager.LoadScene("LeeSangMookPuzzle");
                    break;
                case Stage.Rocket:
                    SceneManager.LoadScene("MaryJacksonPuzzle");
                    break;
                case Stage.Virus://gm.MicroscopeScene();
                    SceneManager.LoadScene("2-1_JuneAlmeidaVirusGame");
                    break;
                default://none, default to cursor locking again JNC
                    Cursor.lockState = CursorLockMode.Confined;//alt is locked
//                    Cursor.visible = false;//don't hide, as it should be visible for menu interfaces
                    break;
            }//end switch statement
            ///error check contingency code?
        }//endif
    }//end OnTriggerEnter

}//end PortraitInteraction class