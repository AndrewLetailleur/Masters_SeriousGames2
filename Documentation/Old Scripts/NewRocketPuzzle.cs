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
*    References: https://answers.unity.com/questions/911194/how-do-i-check-if-an-object-contains-a-child.html
**************************************************/

/// <summary>
/// This script is a custom fix, or hack for a 'drag and test' puzzle, element wise.
/// However, I screwed up with logic. UML cowboy wise. So I've scrapped this code.
/// 
/// As in, it works the first time. But on resetting, it screws up from there.
/// Cowboy me would have bypassed it by just reset scene. But that ain't efficient.
/// </summary>

public class NewRocketPuzzle : MonoBehaviour
{
    //make it more global?
    public Vector3 resetPos;//make it only position, makes it easier on all
    public GameObject heldObj;
    //check if it works public first, before private.

    //enum values, int/numbers as tags wise
    public enum E_State { Empty, Right, Wrong }
         public E_State state;
    public enum E_Type { Puzzle, Reciever, Filled }
         public E_Type type;
    public int order;//enum tag comparison, to lazy int value

    //Awake is called first, when the script instance is being loaded
    private void Awake()
    {
        switch (gameObject.transform.childCount)
        {//Debug.Log("There are " + gameObject.transform.childCount + " Children in " + gameObject.name);
            case 0://size is zero, array wise
                type = E_Type.Puzzle;
                resetPos = transform.position;//jnc
                break;
            default://higher than 0 here
                type = E_Type.Reciever;
                resetPos = gameObject.transform.GetChild(0).transform.position;
                break;
        }//end switch. Does NOT take into account negative sized arrays
    }//end awake

    //OnTriggerEnter2D is called when 2D Collider ENTERS a trigger collider, collider wise.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == E_Type.Reciever)
        {//draggable check
            if (collision.gameObject.tag == "Draggable")
            {//script reference check
                type = E_Type.Filled;
                NewRocketPuzzle refee = collision.gameObject.GetComponent<NewRocketPuzzle>();
                if (refee != null)//error check wise, hack though it may be
                {//puzzle script, function call wise.
                    collision.gameObject.transform.position = gameObject.transform.position;//IE: This position!
                    refee.RecieverCode(refee, collision.gameObject);//refee.heldObj = collision.gameObject;
                 ///type = E_Type.Filled;//fixes a "put two things in one" bug.
                }//endif
            }//endif
        }//endif        //else, no code is called, puzzle type wise ;)
    }//end OnTriggerEnter2D

    ///3D Trigger alt, ignore for now, unless 'switching' back to 3D "Pieces".
    //In which case, lazy take off the "2D" Tag for now, collider wise ;)

    //this function is called, to 'set' the held object on a 'holder' object.
    private void RecieverCode(NewRocketPuzzle refee, GameObject obj)
    {
        refee.heldObj = obj;//Fill in the held obj, for later reference
        heldObj.transform.position = transform.position;//this
        heldObj.tag = "Untagged";//detag to default tag

        if (refee.order != order)
        {//if not an exact pair
            state = E_State.Wrong;//Debug.Log("Answer is Wrong!");
        } else {//if the same
            state = E_State.Right;//Debug.Log("Answer is Right!");
        }//endif
    }//end RecieverCode

    //for answer check, grab if "E_State" != Right, sort of deal.
    public void ResetError()
    {///Debug.LogWarning("Resetting value now");

        //reset value of answer check
        state = E_State.Empty;
        if (heldObj != null) {//only apply if reciever/filled  && type == E_Type.Filled
                              //Debug.Log("null check works");
            
            //if in doubt, go nuclear and reload scene instead. Lazy wise

            


            NewRocketPuzzle heldRef = heldObj.GetComponent<NewRocketPuzzle>();
            heldRef.type = E_Type.Puzzle;//reset value of answer check

            if (resetPos != null) { }//jnc cautious check
            else Debug.LogWarning("variable error - resetPos is null");

            //reset heldObj to (mostly) initial settings
            heldObj.transform.position = resetPos;
            heldObj.tag = "Draggable";
            heldObj = null;
        }//endif
        type = E_Type.Reciever;
    }//end ResetError

    ///end of content

}//End NewRocketPuzzle Class