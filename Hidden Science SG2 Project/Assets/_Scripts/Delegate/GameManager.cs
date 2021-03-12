using System;
using System.Collections;
using System.Collections.Generic;///for List support, Options and Arrays wise
using UnityEngine;
using UnityEngine.SceneManagement;//for scene transitions
/*//future libraries to use, for Options set up/editing.
using UnityEngine.Audio;
using UnityEngine.UI;*/


/**************************************************
*    Title: GameManager
*    Author: Andrew Irvine Letailleur
*    Date: 03/02/2021
*    Code version: 0.1
*    Availability: Here
*    ===============
*    References: Past GameManagers (Which used Brackey's as a source)
**************************************************/
/// <summary>
/// The purpose of the Game Manager script,
/// is to manage the state of the game,
/// whilst it is running. As such,
/// there should only be one Game Manager at any time.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton Setup
    /** there should only be one GameManager existing
    * Possibly better pattern is from this source;
    * https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    */ //if source is implemented. "Public Static" would be the 'instance', while private Static would be the 'Singleton', ideally.
    
    //there can be only one static, stable wise?
    public static GameManager gm;//the Singleton in-class, check. From lecture. Stick with this base, for now
    #endregion
    #region State Setup
    //add in variables for tracking mini-game status, here?
    //this could be 'used' to store progress for journal entries, unlock wise.
    public int score = 0;//not needed, but kept public in cse of third mini-game dependency.
    public bool First, Second, Third = false;//default to "false", unless true for Scene trigger values
    #endregion
    /*Options/etc can wait for later, UI/Audio & Resolution wise*/

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        #region Singleton Pattern
        if (gm != null && gm != this) Destroy(this.gameObject);
        else gm = this; //class, or (this) script.
        DontDestroyOnLoad(this);//to ensure it stays itself.
        //Debug.Log(gm);//end Singleton Pattern, to a hack degree
        #endregion

    }

    //called first, ideally once only
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//add code to load on scene startup
    }
    // called second, or every scene loaded. Debug loop
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HubWorld") { UpdateCheck(); Debug.Log("Exit check is GOOD!"); }
    }

    #region Scene Transition Pattern
    // //Unity REF: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html

    // //to targeted Prototype world
    //     //GUI Check phase. "Check/Load" trigger here?

    //don't use numbers. Use names to identify by name, as numbers can shift. Names cannot

    // //TDL, include Options functions/functionality

    //debug logic to call upon this, to edit/set up the NPC's to interact when needed?
    public void UpdateCheck()
    {
        //blah, get "main menu" script, reference wise. And code from there
            //check on basic set up on "talk" figures/variables for Danica, per say

        if (First) Debug.Log("A");
        if (Second) Debug.Log("BA");
        if (Third) Debug.Log("CAB");
    }


    //******Danica moving stuff around*****
    /*I moved all the content from the MainMenu script here so all the scene management is handled in one location, but I couldn't 
    get the buttons to work in the June Almeida scene using the game manager, so I just left the UI manager object with the MainMenu script
    in that scene alone.
    Could someone else have a go at changing it so it uses the game manager pls?
    
    The scene transition when you go through the portraits is set up to use the game manager now. 
    
    */

    //deprecated function, as it's all auto-called by the "puzzle collider" script
    //public void MicroscopeScene() { SceneManager.LoadScene("2-1_JuneAlmeidaMicroscopeScene"); }

    /*calls upon a targeted scene and failing that; the hub-world. Hindsight, not needed?
    public void GenericScene(string scene_name)
    {
        if (scene_name != null) SceneManager.LoadScene(scene_name);
        else SceneManager.LoadScene("HubWorld");//backup default
    }*/
    public void HubworldScene()//main menu default. 
    {///Tie to lazy scene load for easier programming hassle
        score = 0;//lazy reset
        //increment values
        if (First) score++;
        if (Second) score++;
        if (Third) score++;

        //switch case, for Hubworld load
        switch (score) {
            case 3://last "scene", replayable. But have an 'end convo' in hand.
                Debug.Log("Finale time"); SceneManager.LoadScene("HubWorld");
                //load third scene
                break;
            case 2://third talk, "HubWorld-3" guidance
                Debug.Log("Two... done"); SceneManager.LoadScene("HubWorld");
                //load second scene
                break;
            case 1://second speach, "HubWorld-2" wise
                Debug.Log("One Minigame done"); SceneManager.LoadScene("HubWorld");
                //load first scnee
                break;
            default://"case zero", default menu
                Debug.Log("Scene first loaded");
                SceneManager.LoadScene("HubWorld");
                break;
        }

    SceneManager.LoadScene("HubWorld");
    }
    // //Andrew L Edit = a bit inefficient? So instead, going to try and make a "generic name X" Scene manager, in case it 'just works'.
    // public void GenericScene(string scene_name)
    // { SceneManager.LoadScene(scene_name); }

    // This quits the game and includes a debug message when testing
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }//end QuitGae

    #endregion
}//end GameManager script/class