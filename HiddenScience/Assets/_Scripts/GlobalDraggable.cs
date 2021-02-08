using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDraggable : MonoBehaviour
{
    //a global version, or take 2/etc on the "Global/Universal Drag N Drop" Script
    //in a lens of "try to avoid needing/using the Unity OnMono blah events.
    //as apparently, "OnMouse" events are not global friendly?

    private Vector3 screenPos, offsetPos;

    // Update is called once per frame
    void Update()
    {
        Ray3DCheck();
    }

    void Ray3DCheck() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float dbRayLength = 13f; Debug.DrawRay(ray.origin, ray.direction * dbRayLength, Color.red);
        RaycastHit hit;

            //PARANOIA!
        Physics.Raycast(ray, out hit, Mathf.Infinity);//100f
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Fire!");
            screenPos = Camera.main.WorldToScreenPoint(
                hit.collider.transform.position);
            offsetPos = hit.collider.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));//hit.point;
            //grab/interact object here, on click
        }//end 3D Check?

        if (Input.GetMouseButton(0)) {
            Debug.Log("??Fir??");
            hit.collider.transform.position =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, screenPos.z))
                    + offsetPos;

            //note to self, debug later on on this.


        }

    }


}
