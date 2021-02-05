using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;//For easier inclusion of functionality.
//Old input system is kept-in to be mindful on backwards compatibility, programming wise.

//REFERENCE: https://www.youtube.com/watch?v=55TBhlOt_U8
    //THIS CODE is based on 2D, but could at a stretch, also be used for 3d, in a perspective viewpoint.
//WARNING = going 3d in orthodontic view is NOT Recommended, ATM.
public class LocalDraggable : MonoBehaviour
{
    private bool isDragged = false;//default to false
    public bool ShowDrag(bool isDragged) { return isDragged; }


    //Awake is called when the script instance is being loaded
    void Awake() {
        #region Rigidbody Check
        if (GetComponent<Rigidbody2D>() == null || GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning("For colliders to work with local dragging, a Rigidbody is required");
            Destroy(this);//remove this from object, as it's clearly not usable at this state
            /*this.gameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0f; //disabling gravity, JNC
            */
        }
        #endregion
    }//end Awake

//focus on Mouse short term. See/figure out interactive elements later
#region MouseClicks
    public void OnMouseDown() { isDragged = true; }
    public void OnMouseUp() { isDragged = false; }
    public void OnMouseDrag() {//if dragged.
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



    // Update is called once per frame
    void Update() { }

    public string puzzle = "B";
    //collider check
    private void OnTriggerStay(Collider other) {
        if (!isDragged && other.tag == puzzle)
        { Fusion(gameObject, other.gameObject); }
    }//3d collider version, of code
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("is colliding");
        //check, if not draggin'
        if (!isDragged) {
            //SpriteRenderer bb = this.gameObject.GetComponent<SpriteRenderer>();
            //bb.color = new Color(0, 0, 1);

            if (collision.tag == puzzle)
            { Fusion(gameObject, collision.gameObject); }

        }
    }
    public void Fusion(GameObject a, GameObject b)//GameObject spawn
    {//Buggy 'fusion' code test, that may speculatively work, but ain't tested just yet.
        //GameObject spawn = Instantiate(spawn, b.transform.position, b.transform.rotation);
        Destroy(a); Destroy(b);
        
        GameManager gm = GetComponent<GameManager>();//game manager
        gm.score--;//decrement 'score'. Treat as a 'marker' to track mini-game progress?
        if (gm.score < 1) { Debug.Log("Collision successful"); }
        //trigger "exit scene" and a flag, once 'all' puzzle objects are slotted in, answer wise
    }
}
