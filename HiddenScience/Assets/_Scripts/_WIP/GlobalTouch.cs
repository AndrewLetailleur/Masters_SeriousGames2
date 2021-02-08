using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTouch : MonoBehaviour
{
    private Vector3 screenPos, offsetPos;
    private GameObject handled;
    public GameObject obj;

    // Update is called once per frame
    void Update()
    {
        Ray3DCheck();
        //Ray2DCheck();
    }

    void Ray3DCheck()
    {
        if (Input.touchCount > 0)//|| Input.GetMouseButtonDown(0)
        {
            //setup
            Touch touch = Input.GetTouch(0);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //float dbRayLength = 13f; Debug.DrawRay(ray.origin, ray.direction * dbRayLength, Color.red);

            //raycasting
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                {
                    //difference between object and click pos
                    offsetPos = hit.collider.transform.position -
                        Camera.main.ScreenToWorldPoint(new Vector3(
                        touch.position.x, touch.position.y,
                        screenPos.z));//hit.point;

                    //grab the location of object on screen?
                    screenPos = Camera.main.WorldToScreenPoint(
                        hit.collider.transform.position);

                    //stored items. Not needed, but useful?
                    handled = hit.collider.gameObject;
                }
                //(Input.GetMouseButton(0)
                if (touch.phase == TouchPhase.Moved || 
                    touch.phase == TouchPhase.Stationary) {
                    Vector3 touchScreenPoint = new Vector3(
                        touch.position.x, touch.position.y,
                        screenPos.z - offsetPos.z);

                    hit.collider.gameObject.transform.position
                        = Camera.main.ScreenToWorldPoint(
                        touchScreenPoint) + offsetPos;

                    //store the clicked object?
                        //handled = hit.collider.gameObject;
                }
                //end ifs
            }//end Raycasting
        }//end input touch
    }//end 3D Check
}
