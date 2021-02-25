using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**************************************************
*    Title: LocalPuzzleState
*    Author: Andrew Irvine Letailleur
*    Date: 12/02/2021
*    Code version: 0.5
*    Availability: Here
*    ===============
*    References: None (past experience with instancing. Could be more efficient with actor swapping instead?)
**************************************************/
/// <summary>
/// The purpose of the Local LocalPuzzleState script,
/// Is to be a 'match 2' in functionality.
/// -
/// As such, this is a LOCAL Script.
/// As this compares two actors with the script attached.
/// -
/// This should additionally, give/pull a 'call' on a Game Manager, to set or lower a variable later on.
/// </summary>
public class LocalPuzzleState : MonoBehaviour
{
    //local script, variables wise.
    public GameObject AnswerPiece;
    public string puzzle = "B";//the variable to compare, to another object's tag. For puzzle use mainly.
    private string col_string = "#ERROR";//jnc debug variable
    //  public bool isDraggable, Dragged; ///REDACTED, as "Tag System" (Draggable) works just as fine.
    ///also, if in doubt, a "Local Drag" Script works just as well.



    //collision code, for if object is draggable, and input is not given. NOT "New Input System" Friendly!
    #region OnTrigger Checks
    //3D Logic
    private void OnTriggerStay(Collider col_three)
    {///check input first, safety value wise. And only check if object is draggable as well.
        //string col_string = "#ERROR";//this is private global now
        //Debug.Log("WRYYYYYYYYY");//NEED Rigidbody to collide!
        
        if (col_three.gameObject.GetComponent<LocalPuzzleState>().puzzle != null)
            col_string = col_three.gameObject.GetComponent<LocalPuzzleState>().puzzle;

        //&& Input.GetMouseButtonUp(0)

        if (gameObject.tag == "Draggable")//if this object is draggable check
        {//or "GetTouch(0)" is triggering, to 'held' errata
            Debug.Log("Checking key variable grabs. = " + puzzle + " && " + col_string);
            if (col_string == puzzle)
            { Fusion(gameObject, col_three.gameObject, AnswerPiece); }
        }//end check
    }//3d collider version, of code
    //2D Logic
    private void OnTriggerStay2D(Collider2D col_two)
    {///check input first, safety value wise. And only check if object is draggable as well.
        //string col_string = "#ERROR";//this is private global now
        Debug.Log("WRYYYYYYYYY");

        if (col_two.gameObject.GetComponent<LocalPuzzleState>().puzzle != null)
            col_string = col_two.gameObject.GetComponent<LocalPuzzleState>().puzzle;

        if (gameObject.tag == "Draggable" && Input.GetMouseButtonUp(0))//if this object is draggable check
        {//or "GetTouch(0)" is triggering, to 'held' errata
            //Debug.Log("Checking key variable grabs. = " + puzzle + " && " + col_string);
            if (col_string == puzzle)
            { Fusion(gameObject, col_two.gameObject, AnswerPiece); }
        }//end check
    }//2d collider version, of code
    #endregion
    //check wise, ONLY call fusion script IF object is Draggable, and input is released/not pressed.

    //Sample code, for "visual" indication on a draggable object?
    #region GUI_HighlightTest
    //Fix "Invert Color" check once it can also be 'done' on 3D Models/materials, 'temporary' wise.
    public void OnMouseEnter() { InvertColour(); }
    public void OnMouseExit() { InvertColour(); }
    //flips the color values of the sprite, upon mouse hovering/leaving.
    private void InvertColour()
    {
        if (GetComponent<SpriteRenderer>() != null)//if 2d
        {
            SpriteRenderer mat = gameObject.GetComponent<SpriteRenderer>();
            Color invert = new Color(
            1 - mat.color.r,
            1 - mat.color.g,
            1 - mat.color.b);
            mat.color = invert;
        }//this works, to a hack degree
        else if (GetComponent<Material>() != null) {//if 3dy, consider using MeshRenderer for checks instead
            Material mat = GetComponent<Material>();//the mat to edit hack wise
            Color invert = new Color(
            1 - mat.color.r,
            1 - mat.color.g,
            1 - mat.color.b);
            mat.color = invert;
        }//this MAY work, need to check/try out
        //end ifs
    }//end InvertColor
    #endregion

    //fuses objects together, if condition is triggered in puzzle element/s
    public void Fusion(GameObject a, GameObject b, GameObject answer)//GameObject spawn
    {//Buggy 'fusion' code test, that may speculatively work, but ain't tested just yet.
        GameObject spawn;
        if (answer != null) { spawn = Instantiate(answer, b.transform.position, b.transform.rotation); }
        else { Debug.Log("No answer piece is detected. As such, just going to remove the collisions instead"); }
        //spawn is there, in case further adjustment's to code is needed

        //reference the game manager
        GameManager gm = GetComponent<GameManager>();//game manager
        if (gm != null)
        {
            gm.score--;//decrement 'score'. Treat as a 'marker' to track mini-game progress?
            Debug.Log("Score Deduction leads to GM Score at " + gm.score);
            if (gm.score < 1) { Debug.Log("Collision successful"); }
            //trigger "exit scene" and a flag, once 'all' puzzle objects are slotted in, answer wise
        }
        //end code
        Destroy(a); Destroy(b);//or "set inactive"
    }//end Fusion

}
