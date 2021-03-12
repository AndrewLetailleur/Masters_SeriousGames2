using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: GlobalDraggable
*    Author: Andrew Irvine Letailleur
*    Date: 10/02/2021
*    Code version: 1.0 (Take 2)
*    Availability: Here
*    ===============
*    References: HCI Tutorials + Local Draggable (give or take, parsing/referring to past code)
**************************************************/

/// <summary>
/// This is 'take 2', under "try to avoid needing/using OnMono events.
/// as apparently, "OnMouse" events are not global friendly? Unity blah wise.
/// </summary>
public class GlobalDraggable : MonoBehaviour
{
        //global variables should track the input position, and offset of touched object
    private Vector3 screenPos, offsetPos;
    //private Touch touch;//in case it's required for easier/quicker reference to touch input? //WIP!!!
//    private GameObject handled;//    public GameObject obj;//only include these objects, if required

    //Awake is called first, when the script instance is being loaded
        //Touch Code is buggy ATM, don't include!//private void Awake() { touch = Input.GetTouch(0); }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) RayTest(Input.mousePosition);//covers both 2D & 3D Checks, raycast wise
        //Ray2D_Drag(Input.mousePosition);//Ray3D_Drag(Input.mousePosition);
        
            ///_Touch, Experimental. See if it can be implemented as a 'generic' check instead?
        //if (Input.touchCount > 0 ) //Ray3D_Drag(touch.position);

        //alternatively, 'track' a sort of boolean value? Or 'state' value of "not pressed, started & during" stage/s?
    }//end update
    #region Raycasting Code
    /// <summary> Dev Notes: potential scroll effect, lazy wise.
    /// thank this source for ref; https://www.youtube.com/watch?v=0yHBDZHLRbQ
    /// =====
    /* float zPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(
    Input.mousePosition.x, Input.mousePosition.y, zPoint));//transform.position.z
    point.z += Input.mouseScrollDelta.y * 3f; //the scroll wheel effect, depending on player input on scroll wheel
    transform.position = point;// new Vector3(point.x, point.y, transform.position.z);
    */
    /// </summary>

    //checks which raycast hits, before performing code relating to that one raycast. 2d/3d wise.
    void RayTest(Vector3 input)
    {
        //3d check
        Ray ray = Camera.main.ScreenPointToRay(input);
        RaycastHit hit3d;
        Physics.Raycast(ray, out hit3d, Mathf.Infinity);//100f
        //float dbRayL = 13f; Debug.DrawRay //(ray.origin, ray.direction * dbRayL, Color.red);

        //try-catch check, to avoid errors. Check if object's hit
        if (hit3d.collider != null && hit3d.collider.tag == "Draggable")//"draggable" tag is optional in this check
            Ray3D_Drag(input, hit3d);//Debug.Log("3d win"); 

        //2d check
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(
            Camera.main.ScreenPointToRay(input));
        //debug code to avoid errors, test/checker wise? But assume it's linked/synced to input?
        if (hit2d.collider != null && hit2d.collider.tag == "Draggable")//"draggable" tag is optional in this check
        { Ray2D_Drag(input, hit2d); }//Debug.Log("2d win"); 
    }//end RayTest

    /*Simplified "Scroll" sample code post check?" Not supported or implemented on Global just yet
    Vector3 point = hit.transform.position;//thank this source for ref; https://www.youtube.com/watch?v=0yHBDZHLRbQ
    point.z += Input.mouseScrollDelta.y * 0.3f;
    hit.collider.transform.position = point;*/

    //the 2D Raycast, that only works in orthographic view, Global wise.
    ///Add 3D Colliders on 2D sprites for perspective view hack.
    void Ray2D_Drag(Vector3 input, RaycastHit2D hit)
    {
        //https://forum.unity.com/threads/unity-2d-raycast-from-mouse-to-screen.211708/
        //Physics2D.GetRayIntersection(
        //RaycastHit2D hit = Physics2D.GetRayIntersection( Camera.main.ScreenPointToRay(input));
        //Ray2D ray = Camera.main.ScreenPointToRay(input);
        //Debug.DrawLine(Vector3.zero, hit2d.transform.position, Color.green);
        /// Debug Drawline is "buggy", but it "just works" in showing for now.

        //how to move code script/etc
        //hit.transform.position = position
        ///===========================
        //A: Get mouse position (local)
        //B: Drag blah to there, by X/Y Co-ordinates,
        //C: IGNORE the Z, bar scroll?

        if (hit.collider != null && hit.collider.tag == "Draggable")
        {///Debug.Log("Squares not included in dragging yet!");

            //Rigidbodies are always moved in world space
            //ergo? Think on "how to keep it to 2D Only", X/Y wise
            if (Camera.main.orthographic)
            {
                Vector2 position2D = Camera.main.ScreenToWorldPoint
                (input) - hit.transform.position;
                hit.transform.Translate(position2D);
            }
            else
            {
                Debug.LogWarning("Perspective view just doesn't work ATM. Please switch to another view angle for 2D Manipulations");
                ///give up on this part of the code
                /*float TempZ = hit.transform.position.z;
                Vector3 offset2D = Camera.main.ScreenToWorldPoint(input);
                var dist = offset2D - hit.transform.position;
                hit.transform.position += new Vector3(
                    dist.x, dist.y, 10);
                */
            }//end camera check
        }//end collider code
    }//end Ray2D_Drag

    ///reference for 'lazy scroll code = += Input.mouseScrollDelta.y * 3f

    //the 3D Raycast, that "Just Works"
    void Ray3D_Drag(Vector3 input, RaycastHit hit) {//, Ray ray 
        //"input" Vector = universal touch position, Fix Later wise

            ///not needed, why cast twice, when once suffices?
        //Physics.Raycast(ray, out hit, Mathf.Infinity);//100f
            //float dbRayL = 13f; Debug.DrawRay //
        //(ray.origin, ray.direction * dbRayL, Color.red);

            /// "Magic Tape", er, Raycasting Wizardry! (BE CAREFUL ON EDITING THIS!)
        //try-catch check, to avoid errors. Check if object's hit
        if (hit.collider != null && hit.collider.tag == "Draggable")
        {///add tag conditions on top, to ensure only X objects are dragged.

            //|| touch.phase == TouchPhase.Began ///proto touch support add-on
            if (Input.GetMouseButtonDown(0))//start mouse check
            {///Debug.Log("Catch!");
                //grab the location of object on screen?
                screenPos = Camera.main.WorldToScreenPoint(
                    hit.collider.transform.position);
                
                //difference between object and click pos
                offsetPos = hit.collider.transform.position -
                    Camera.main.ScreenToWorldPoint(new Vector3(
                        input.x, input.y, screenPos.z));//hit.point;
                //grab/interact object here, on click
                    //handled = hit.collider.gameObject;
            }//end start input collider

            //|| touch.phase == TouchPhase.Moved        ///proto touch support add-on
            //|| touch.phase == TouchPhase.Stationary   ///proto touch support add-on
            if (Input.GetMouseButton(0))//drag check
            {///Debug.Log("Drag-On!"); //note to self, debug later on on this if required.
                hit.collider.transform.position =
                    Camera.main.ScreenToWorldPoint(
                        new Vector3(input.x, input.y,
                        screenPos.z)) + offsetPos;//this formula just works
            }//end drag input collider
        }//end hit collider != null check
    }//end Ray3D_Drag script
#endregion
}//end GlobalDraggable Class