using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: PuzzlePiece
*    Author: Andrew Irvine Letailleur
*    Date: 09/03/2021
*    Code version: 1b
*    Availability: Here
*    ===============
*    References: PuzzleSlot ("parent")
**************************************************/

/// <summary>
/// This script is a "tiny" split from the Puzzle Slot script.
/// ===
/// IE: This used to be one script, but bugs happaned.
/// So, splitting it into different scripts by function, worked to debug.
/// </summary>

public class PiecePuzzle : MonoBehaviour
{
    public int order;//enum tag comparison, to lazy int value
     
    //debug
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collider has desired Script, puzzle wise. Then it's a "Puzzle" tag.
        if (collision.gameObject.GetComponent<PuzzleSlot>() != null)
        {
            PuzzleSlot slot = collision.gameObject.GetComponent<PuzzleSlot>();
            if (slot.heldObj == null)
            {///if empty, add puzzle Piece in, and check with script
                gameObject.transform.position = collision.gameObject.transform.position;
                slot.FillObject(gameObject, order);
            }//endif
        }//endif
    }//end onTriggerEnter2D

    //TDL? include OnTriggerEnter, 3D Logic wise here later.
}//end PiecePuzzle class