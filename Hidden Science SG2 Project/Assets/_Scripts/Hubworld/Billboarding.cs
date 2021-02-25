/* Danica's script

This is for making 2D sprites look at the camera at all times
Followed tutorial by gamesplusjames: https://www.youtube.com/watch?v=_LRZcmX_xw0

NPC character sprites- set useStaticBillboard to true in editor (makes them move aside from camera view when player approaches)
Environment sprites - set useStaticBillboard to false in editor (makes them stationary)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera myCamera;
    public bool useStaticBillboard;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(myCamera.transform);   
        } else
        {
           transform.rotation = myCamera.transform.rotation; 
        }
        
        transform.rotation = Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f);
    }
}
