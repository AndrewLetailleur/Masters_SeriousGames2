//Danica's script (based on Brackeys tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&list=LL&index=7)
//This controls the main camera movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
*    Title: MouseLook
*    Author: Danica
*    >Edited/tweaked by, Andrew Letailleur
*    Date: 10/03/2021
*    Code version: 1.1 (Tweak)
*    Availability: Here
*    ===============
*    References: switch case logic, set up wise
**************************************************/

/// <summary>
/// The purpose of this script, is [BLAH]
/// =
/// PS: Debugged code is fixed, hidden cursor wise.
/// Further tweaks are parraell/passed with the
/// "PortraitInteraction" script
/// </summary>
public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //added some comments here, for future editing REF = AIL
       Cursor.lockState = CursorLockMode.Confined;//locks the cursor at start
       Cursor.visible = true;//false;//hides the cursor from view, jnc
    }//end Start

    // Update is called once per frame
    void Update()
    {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation,-90f,90f);

      transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
      playerBody.Rotate(Vector3.up * mouseX);
    
    }//end Update
}//end MouseLook class