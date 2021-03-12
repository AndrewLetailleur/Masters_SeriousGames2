using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: AnswerCheck
*    Author: Andrew Irvine Letailleur
*    Date: 09/03/2021
*    Code version: 1.1 (Take 3)
*    Availability: Here
*    ===============
*    References: Past experience with button scripts.
**************************************************/

/// <summary>
/// The purpose of this script, is to be attached to the 'check' button
/// Only performs local features. For global "scene" management, use GameManager instead.
/// -
/// PS: Rewrote this again, if only to debug and simplify code
/// </summary>

public class Check_Puzzle : MonoBehaviour
{
    //the held items to check. Assign manually for now. Ideally, automate it
    public GameObject[] checks;

    //Awake is called first, when the script instance is being loaded
    private void Awake()
    {
        if (checks == null)//exit game if true?
        {///Debug.LogError("you forgot a critical component, deleting this now to bar further issues");
            Destroy(this);//remove this component, as it won't work.
        }//endif
    }//end Awake

    //This checks if the question is right or wrong
    public void ButtonCheck()
    {
        bool wrong = false;//start false, until proven otherwise
             //int wInt = 0;//for later "count by", debug wise

        //a different for, to check two conditions instead?
        for (int i = 0; i < checks.Length; i++)
        {
            bool booly = checks[i].
                GetComponent<PuzzleSlot>()
                .FailCheck();//refer to the code itself only
            if (booly)//if script can be grabbed by i
            { wrong = true; }//nothing
            //endif
        }//end for

        //if wrong, reset all held object positions to default
        if (wrong)///Debug.Log("Answer is wrong! by " + wInt);
        {
            for (int i = 0; i < checks.Length; i++)
            {
                checks[i].GetComponent<PuzzleSlot>().ResetPos();
            }//end for
        } else {
            MainMenu mm = FindObjectOfType<MainMenu>();//for "Your winner" script
            mm.HubworldReturn(3);//UpdateCheck(); give/take "mark third as true"
            Debug.Log("You're Winner!");//AnswerGotten();
        } //endif
    }//end New Button Check
}//end Check_Puzzle class