using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;//For easier inclusion of functionality.
    //disabled for now, until it's fully supported in-script
//Old input system is kept-in to be mindful on backwards compatibility, programming wise.

//REFERENCE: https://www.youtube.com/watch?v=55TBhlOt_U8
//THIS CODE is based on 2D, but could at a stretch, also be used for 3d, in a perspective viewpoint.
//WARNING = going 3d in orthodontic view is NOT Recommended, ATM.

/**************************************************
*    Title: LocalDraggable
*    Author: Andrew Irvine Letailleur
*    Date: 07/02/2021
*    Code version: 0.9
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
    private bool isDragged = false;//default to false
    public bool ShowDrag(bool isDragged) { return isDragged; }
    public string puzzle = "B";//the variable to compare, to another object's tag. For puzzle use mainly.
    public GameObject FilledPiece;

    //Awake is called when the script instance is being loaded
    void Awake() {
#region Rigidbody Check
        if (GetComponent<Rigidbody2D>() == null || GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning("For colliders to work with local dragging, a Rigidbody is required. Checking Rigidbody now...");
            RigidbodyDebug();
        }
#endregion
    //check for any collider first, if statement wise
    }//end Awake

//add a rigidbody if viable, and remove the object if unviable.
#region Rigidbody Debug
    private void RigidbodyDebug()
    {//check for any collider first, if statement wise
        if (GetComponent<Collider>())
        {
            Rigidbody rb3d = gameObject.AddComponent<Rigidbody>();
            rb3d.useGravity = false;
            Debug.Log("Added a 3D Rigidbody in, hack wise");
        }
        else if (GetComponent<Collider2D>())
        {
            Rigidbody2D rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0F;//disabling gravity, JNC
            Debug.Log("Added a 2D Rigidbody in, hack wise");
        }
        else
        {
            Debug.LogWarning("Destroying this object from scene, as failsafe code failed to implement.");
            Destroy(this);
        }
    }
#endregion
 
//focus on old input system short term. See/figure out interactive elements for new input system later
#region MouseClicks
    public void OnMouseDown() { isDragged = true; }
    public void OnMouseUp() { isDragged = false; }
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
            }
        }
    }
#endregion
//ps note: 2d has some 'slight' bugs, work on them later!

    //Sample code, for "visual" indication on a draggable object?
    #region GUI_HighlightTest
    //Fix "Invert Color" check once it can also be 'done' on 3D Models/materials, 'temporary' wise.
    public void OnMouseEnter() { InvertColour(); }
    public void OnMouseExit() { InvertColour(); }
    //flips the color values of the sprite, upon mouse hovering/leaving.
    private void InvertColour()
    {
        if (GetComponent<SpriteRenderer>() != null) {
            SpriteRenderer bb = gameObject.GetComponent<SpriteRenderer>();
            Color invert = new Color(
            1 - bb.color.r,
            1 - bb.color.g,
            1 - bb.color.b);
            bb.color = invert;
        } else if (GetComponent<Material>() != null) { }//TODO, "Invert material, in color scheme/etc
        //end ifs
    }//end InvertColor
#endregion

    // Update is called once per frame   void Update() { }

    //collider check
    private void OnTriggerStay(Collider other) {
        if (!isDragged && other.tag == puzzle)
        { Fusion(gameObject, other.gameObject, FilledPiece); }
    }//3d collider version, of code
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("is colliding");
        //check, if not draggin'
        if (!isDragged) {
            //SpriteRenderer bb = this.gameObject.GetComponent<SpriteRenderer>();
            //bb.color = new Color(0, 0, 1);

            if (collision.tag == puzzle)
            { Fusion(gameObject, collision.gameObject, FilledPiece); }

        }
    }
    public void Fusion(GameObject a, GameObject b, GameObject z)//GameObject spawn
    {//Buggy 'fusion' code test, that may speculatively work, but ain't tested just yet.
        GameObject spawn = Instantiate(z, b.transform.position, b.transform.rotation);        
        GameManager gm = GetComponent<GameManager>();//game manager
        gm.score--;//decrement 'score'. Treat as a 'marker' to track mini-game progress?
        if (gm.score < 1) { Debug.Log("Collision successful"); }
        //trigger "exit scene" and a flag, once 'all' puzzle objects are slotted in, answer wise

        Destroy(a); Destroy(b);//or "set inactive"
    }
}
