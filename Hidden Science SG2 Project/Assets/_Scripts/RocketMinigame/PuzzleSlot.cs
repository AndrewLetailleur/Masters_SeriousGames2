using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: PuzzlePiece
*    Author: Andrew Irvine Letailleur
*    Date: 09/03/2021
*    Code version: 2.0 (Take 3)
*    Availability: Here
*    ===============
*    References: 
**************************************************/

/// <summary>
/// This script checks, and sets puzzle objects, before checking if it's in the right positoin
/// Had to rewrite this one at one point, if only to split apart some functionality to bar bugs
/// Still has some quirks. But it's far better in hindsight, to let it be for now.
/// </summary>

public class PuzzleSlot : MonoBehaviour
{
    public int order;//enum tag comparison, to lazy int value
    //puzzle slot variables
    public Vector3 resetPos;//make it only position, makes it easier on all
    public GameObject heldObj;

    //variables for ONLY the Puzzle Slot.
    public enum E_State { Empty, Right, Wrong }
    public E_State state;
    //Compared to Version 2; no need for "type", as script does it.

    //Awake is called first, when the script instance is being loaded
    private void Awake()
    {
        switch (gameObject.transform.childCount)
        {//to consider; minus values. But childs don't have negative sizes. So eh, a pass.
            case 0://error time
                Debug.LogError("Puzzle Slot does not have a reset transform. Ending now");//error time
                break;
            default://1st child only, 0th wise
                resetPos = gameObject.transform.GetChild(0).transform.position;//set reset position
                break;
            //end case
        }//end switch
    }//end awake

    //when called, uses referenced values, and 'sets'/locks their position, drag wise.
    public void FillObject(GameObject refObj, int refOrder)
    {
        heldObj = refObj;//set reference to heldObj
        heldObj.tag = "Untagged";//makes sure the heldObj is undraggable atm.
        
        //no fill check. Only 'is it right or wrong' check if called upon.
        if (refOrder != order)
        {//if not an exact pair
            state = E_State.Wrong;//Debug.Log("Answer is Wrong!");
        } else {//if the same
            state = E_State.Right;//Debug.Log("Answer is Right!");
        }//endif
    }//end FillObject

    public bool FailCheck()//false if right, true if wrong
    {
        if (heldObj != null)
        {
            if (state == E_State.Right)
            {//correct answer value
                //state = E_State.Empty;
                return false;
            } else {//if E_State.Wrong;
                state = E_State.Empty;
                return true;
            }//endif
        } else {//state = E_State.Empty;
            return true;
        } //presume auto-fail script
        //end condition == > state = E_State.Empty;
    }//end FailCheck

    //reset position if failled value?
    public void ResetPos()
    {
        if (heldObj != null)
        {
            heldObj.transform.position = resetPos;
            heldObj.tag = "Draggable";
            heldObj = null;
        }//endif
    }//end ResetPos
}//end PuzzleSlot class
