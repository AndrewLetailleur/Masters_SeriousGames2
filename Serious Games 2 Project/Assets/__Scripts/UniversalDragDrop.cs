using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;//For easier inclusion of functionality. Disabled for now
//Old input system is kept-in to be mindful on backwards compatibility, programming wise.

///name to be fixed at a later date, rename title and class wise
/**************************************************
*    Title: UniversalDragDrop
*    Author: Andrew Irvine Letailleur
*    Date: 03/02/2021
*    Code version: 0.1
*    Availability: Here
*    ===============
*    References: Past GameManagers (Which used Brackey's as a source)
**************************************************/
/// <summary>
/// The purpose of the "Mouse" Drag script,
/// is to manage mouse interface in a 'universal' layout
/// Ideally, 'via' camera bounds. And scalable, to a 2D or 3D Lens.
/// In words of Sauron;
/// "..One Ring to bring them all, and in darkness bind them."
/// </summary>
public class UniversalDragDrop : MonoBehaviour
{

    /*
    #region Singleton Setup
    // there should only be one Dragger existing, if going universal
    // Possibly better pattern is from this source; https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    // //if source is implemented. "Public Static" would be the 'instance', while private Static would be the 'Singleton', ideally.

    //there can be only one static, stable wise?
        //MAKE Sure, the "static" to refer to, is the same class as the "public class" script, and before MonoBehavior (if implemented)
    public static UniversalDragDrop dragger;//the Singleton in-class, check. From lecture. Stick with this base, for now
    #endregion
    #region State Setup
    //add in variables for tracking mini-game status, here?
    //this could be 'used' to store progress for journal entries, unlock wise.
    #endregion
    
    */
    /*Options/etc can wait for later, UI/Audio & Resolution wise*/

    //public InputAction LeftClickHold; //disabled for now
    public Vector2 mouseDelta;
    public GameObject draggable;//the object TO drag, debug ref wise
    public Vector3 objectPos;

    public Vector3 curMousePos;
    //Awake is called when the script instance is being loaded
    void Awake() {
        /*
        #region Singleton Pattern
        if (dragger != null && dragger != this)
            Destroy(this.gameObject);
        else dragger = this; //class, or (this) script.
        DontDestroyOnLoad(this);//to ensure it stays itself.
        //Debug.Log(gm);//end Singleton Pattern, to a hack degree
        #endregion
        */
        
    }//end Awake

    // Update is called once per frame void Update() { }
    void Update() {
        curMousePos = Input.mousePosition;
    }

    //old input system logic
    public void OnMouseDown() { FireCheck(); Debug.Log("Pressed primary button."); }
    public void OnMouseUp() {
        if (draggable != null) {
            draggable = null;
        }
    }
    public void OnMouseDrag()
    {//if dragged.
        if (draggable != null)
        {
            Debug.Log("Draggin a line");
            draggable.transform.position = Input.mousePosition;
            /*
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
                Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);*/
        }
    }



    private void FireCheck() {
        Debug.Log("Fire!");
        //3D Debug
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100f);
        if (hit.collider != null)
        {
            Debug.Log("Hit!");
            if (hit.collider.tag == "Player") {
                draggable = hit.collider.gameObject;
            }

            //grab/interact object here, on click
        }
        //2D Debug //https://forum.unity.com/threads/unity-2d-raycast-from-mouse-to-screen.211708/
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));//zero, as it's right "at" the position
        //Debug.DrawLine(Vector3.zero, hit2d.transform.position, Color.green);
        //Debug Drawline is "buggy", but it "just works" in showing for now.

        if (hit2d.collider != null)
        {
            Debug.Log("Hit 2d!");
            //grab/interact object here, on click
            if (hit2d.collider.tag == "Player")
            {
                draggable = hit.collider.gameObject;
            }
        }
    }
    private void DragCheck() { }

    /*//new WIP input system variables. Make code to take into account 2D and 3D, and test it's parsed properly here.
    public void OnMouseMovement(InputValue input) { }//2D
    public void OnScrollMovement() { }//3D
    public void OnLeftClick() { }//drag
    public void OnLeftClickHold() {}
    */
}
