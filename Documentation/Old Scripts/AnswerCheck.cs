using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: AnswerCheck
*    Author: Andrew Irvine Letailleur
*    Date: 05/03/2021
*    Code version: 1.0 (Take 2)
*    Availability: Here
*    ===============
*    References: Past experience with button scripts.
**************************************************/

/// <summary>
/// The purpose of this script, is to be attached to the 'check' button
/// Only performs local features. For global "scene" management, use GameManager instead.
/// </summary>
public class AnswerCheck : MonoBehaviour
{
    //the held items to check. Assign manually for now. Ideally, automate it
    public GameObject[] checks;

    //Awake is called first, when the script instance is being loaded
    private void Awake()
    {
        //likely throw
        if (checks == null)
        {//ideally, hack wise. Get ALL The untagged objects, that have a script, and is marked "untagged".
            Debug.LogError("you forgot a critical component, deleting this now to bar further issues");
            Destroy(this);//remove this component, as it won't work.
        }//endif //exit game if true?
    }//end Awake

    //This checks if the question is right or wrong
    public void ButtonCheck()
    {
        bool wrong = false;//start false, until proven otherwise
            //int wInt = 0;//for later "count by", debug wise

        //a different for, to check two conditions instead?
        for (int i = 0; i < checks.Length; i++)
        {    
            var heldObj = checks[i].
                GetComponent<NewRocketPuzzle>().heldObj;
            if (heldObj != null)//if script can be grabbed by i
            {///Debug.Log("First Check win!");
                NewRocketPuzzle.E_State stete = heldObj.
                    GetComponent<NewRocketPuzzle>().state;
                //default to "empty"
                if (stete == NewRocketPuzzle.E_State.Right) { }//Debug.Log("Second Check win!"); //alt is c.state?
                else { wrong = true; }//wInt++; 
            } else {//set to wrong if held is empty
                wrong = true;// wInt++; Debug.Log("Tis' Empty!");
            }//endif
        }//end for

        //if wrong, reset all held object positions to default
        if (wrong)//Debug.Log("Wrong is set to ; " + wrong.ToString());
        {///Debug.Log("Answer is wrong! by " + wInt);
            for (int i = 0; i < checks.Length; i++)
            {//swear by var, decode wise    //Debug.Log("Do I print?");
                var c = checks[i].GetComponent<NewRocketPuzzle>();
                c.ResetError(); //this method self checks if it's a "filled" reciever, which it should be!
                //endif
            }//end for
        } else {
            AnswerGotten();
        } //endif
    }//end ButtonCheck
    //if in doubt, "reset scene" is lazy code, that might work?

    //trigger "YOU WINNER" UI from this method!
    public void AnswerGotten() { Debug.Log("All is answered"); }//end AnswerGotten
    ///EXPAND!

    ///end of content

}//End AnswerCheck Class