using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;//For easier inclusion of functionality.
    //disabled for now, until it's fully supported in-script
//Old input system is kept-in to be mindful on backwards compatibility & ease, programming wise.

//REFERENCE: https://www.youtube.com/watch?v=55TBhlOt_U8
//THIS CODE is based on 2D, but could at a stretch, also be used for 3d, in a perspective viewpoint.
//WARNING = going 3d in orthodontic view is NOT Recommended, local wise.

/**************************************************
*    Title: LocalDraggable
*    Author: Andrew Irvine Letailleur
*    Date: 10/02/2021
*    Code version: 1
*    Availability: Here
*    ===============
*    References: https://www.youtube.com/watch?v=55TBhlOt_U8
*    THIS CODE is based on 2D, but could at a stretch, also be used for 3d,
*    Under a perspective viewpoint.
*    WARNING = going 3d in orthodontic view is NOT Recommended, ATM.?
**************************************************/
/// <summary>
/// The purpose of the "Mouse" Drag script,
/// is to make it simple to implement in a "local" scale
/// Though this is (mostly) scalable from 2d/3d angles, it ain't perfect.
/// -
/// Also, on the "todo" list, IMPLEMENT FULL SUPPORT for the New Input System!
/// </summary>

public class LocalDraggable : MonoBehaviour
{
    //global variables should track if the object's being dragged or not, to a protected degree.
    private bool isDragged = false;//default to false
    public bool ShowDrag(bool isDragged) { return isDragged; }

    /*REDACT Puzzle variables, they should be on a seperate 'collider' instead.*/

    //#region Rigidbody Debug = Debug code just don't work, so REDACT
    
    //focus on old input system short term. See/figure out interactive elements for new input system later
    #region MouseClicks
    //OnMono events "just work", under a local lens. Event wise
    public void OnMouseDown() { isDragged = true; }//end Event
    public void OnMouseUp() { isDragged = false; }//end Event
    public void OnMouseDrag() {//if dragged. 2D Mode may need some debug, but mostly works fine, on a 2d/perspective plane!
        if (isDragged) {
                //debug the orthographic view, to be "alternate camera point" friendly, local rotate wise
            if (Camera.main.orthographic) { //ie: IF Camera is "2D view
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint
                (Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            } else { //scroll effect, lazy wise Debug.Log("Currently in an unsupported, local camera view");
                float zPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;//thank this source for ref; https://www.youtube.com/watch?v=0yHBDZHLRbQ
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPoint));//transform.position.z
                point.z += Input.mouseScrollDelta.y * 3f;
                transform.position = point;// new Vector3(point.x, point.y, transform.position.z);
            }//End Camera Code
        }//end Drag Check
    }//end Event
    #endregion
    //ps note: 2d has some 'slight' bugs, work on them later!

    //collider check's not needed, as that's being handled by "LocalPuzzleState"
}//end LocalDraggable Class