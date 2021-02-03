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

    #region Scene Transition Pattern
    //Unity REF: https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html

    //to targeted Prototype world
        //GUI Check phase. "Check/Load" trigger here?

    //"SceneName" should be given, from the object calling upon this function
    public void LoadMinigame(string SceneName) {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        //debug log note/check. Consider Additive instead?
    }
    //Menu Return
    public void LoadStartMenu() { SceneManager.LoadScene(0); }

    //Hub World Return
    public void LoadHubWorld() { SceneManager.LoadScene(1); }
    #endregion

    //TDL, include Options functions/functionality

    // Update is called once per frame. Unneeded for now void Update(){}
}
