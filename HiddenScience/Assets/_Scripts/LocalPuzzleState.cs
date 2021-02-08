using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPuzzleState : MonoBehaviour
{
    //test script, jnc of local variables, yadda yadda.

    public bool isDraggable, Dragged;
    // Start is called before the first frame update
    //void Start() { }
    // Update is called once per frame
    //void Update() { }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Dragged) { }
    }//private void OnTriggerStay(Collider other) { }
}
