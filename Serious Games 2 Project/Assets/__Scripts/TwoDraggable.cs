using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;//For easier inclusion of functionality.
//Old input system is kept-in to be mindful on backwards compatibility, programming wise.

//REFERENCE: https://www.youtube.com/watch?v=55TBhlOt_U8
//WARNING = THIS CODE is atm, useful for ONLY Orthographic view
public class TwoDraggable : MonoBehaviour
{
    private bool isDragged = false;//default to false
    public bool ShowDrag(bool isDragged) {
        return isDragged;
    }


    //Awake is called when the script instance is being loaded
    void Awake() {
        #region Rigidbody2D Check
        if (GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogWarning("For colliders to work, Rigidbody(2D) is required");
            this.gameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0f; //disabling gravity, JNC
        }
        #endregion
    }//end Awake

//focus on Mouse short term. See/figure out interactive elements later
#region MouseClicks
    public void OnMouseDown() { isDragged = true; }
    public void OnMouseUp() { isDragged = false; }
    public void OnMouseDrag() {//if dragged.
        if (isDragged) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
                Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
    #endregion

//Sample code, for "visual" indication on a draggable object?
#region GUI_HighlightTest
    public void OnMouseEnter() { InvertColour(); }
    public void OnMouseExit() { InvertColour(); }
    //flips the color values of the sprite, upon mouse hovering/leaving.
    private void InvertColour() {
        SpriteRenderer bb = this.gameObject.GetComponent<SpriteRenderer>();
        Color invert = new Color(
            1 - bb.color.r,
            1 - bb.color.g,
            1 - bb.color.b);
        bb.color = invert;
    }//end InvertColor
#endregion

    // Update is called once per frame
    void Update() { }

    //collider check
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("is colliding");
        //check, if not draggin'
        if (!isDragged) {
            SpriteRenderer bb = this.gameObject.GetComponent<SpriteRenderer>();
            bb.color = new Color(0, 0, 1);

            /*Buggy 'fusion' code test, that doesn't work ATM
            if (collision.CompareTag(gameObject.tag) &&
                collision.GetComponent<TwoDraggable>().ShowDrag(false)
                )
            {
                Debug.Log("Removing debug test");
                Destroy(this.gameObject);
            }*/
        }


    }
}
