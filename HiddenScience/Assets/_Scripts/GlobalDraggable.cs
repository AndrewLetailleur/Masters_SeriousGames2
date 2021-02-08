using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDraggable : MonoBehaviour
{
    //a global version, or take 2/etc on the "Global/Universal Drag N Drop" Script
    //in a lens of "try to avoid needing/using the Unity OnMono blah events.
    //as apparently, "OnMouse" events are not global friendly?

    private Vector3 screenPos, offsetPos;
    //private Touch touch;
//    private GameObject handled;
//    public GameObject obj;

    //Awake is called when the script instance is being loaded
    //Touch Code is buggy ATM, don't include!//private void Awake() { touch = Input.GetTouch(0); }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) Ray2D_Drag(Input.mousePosition);
        if (Input.GetMouseButton(0)) Ray3D_Drag(Input.mousePosition);
        //_Touch, Experimental
            //if (Input.touchCount > 0 )
            //Ray3D_Drag(touch.position);
    }

    #region Raycasting Code

    /* //scroll effect, lazy wise Debug.Log("Currently in an unsupported, local camera view");
     float zPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;//thank this source for ref; https://www.youtube.com/watch?v=0yHBDZHLRbQ
     Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPoint));//transform.position.z
     point.z += Input.mouseScrollDelta.y * 3f;
     transform.position = point;// new Vector3(point.x, point.y, transform.position.z);
     */

    void Ray2D_Drag(Vector3 input)
    {
        //https://forum.unity.com/threads/unity-2d-raycast-from-mouse-to-screen.211708/
            //Physics2D.GetRayIntersection(
        RaycastHit2D hit = Physics2D.GetRayIntersection(
            Camera.main.ScreenPointToRay(input));
        //Ray2D ray = Camera.main.ScreenPointToRay(input);
        //Debug.DrawLine(Vector3.zero, hit2d.transform.position, Color.green);
        /// Debug Drawline is "buggy", but it "just works" in showing for now.

        if (hit.collider != null && hit.collider.tag == "Draggable")
        {
            Debug.Log("Squares not included in dragging yet!");
        }
    }
    void Ray3D_Drag(Vector3 input) {
        //"input" Vector = universal touch position, Fix Later wise
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(input);
        //float dbRayL = 13f; Debug.DrawRay
        //(ray.origin, ray.direction * dbRayL, Color.red);

        /// "Magic Tape", er, Raycasting Wizardry! (BE CAREFUL ON EDITING THIS!)
        Physics.Raycast(ray, out hit, Mathf.Infinity);//100f
        //try-catch check, to avoid errors. Check if object's hit
        if (hit.collider != null && hit.collider.tag == "Draggable")
        {
            //add tag conditions on top, to ensure only X objects are dragged.

            //|| touch.phase == TouchPhase.Began
            if (Input.GetMouseButtonDown(0))//start check
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

            //|| touch.phase == TouchPhase.Moved
            //|| touch.phase == TouchPhase.Stationary
            if (Input.GetMouseButton(0))//drag check
            {///Debug.Log("Drag-On!");
                hit.collider.transform.position =
                    Camera.main.ScreenToWorldPoint(
                        new Vector3(input.x, input.y,
                        screenPos.z)) + offsetPos;
                //note to self, debug later on on this.
            }//end drag input collider
        }//end hit collider != null check
    //
    }//end Ray3D_Drag script

#endregion
}
