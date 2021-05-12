//Danica's script (based on Brackeys tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&list=LL&index=7)
//This controls the main camera movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edit by Andrew: Debugged code is fixed, hidden cursor wise. Further tweaks are parallel/passed with the "PortraitInteraction" script
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Confined;//locks the cursor at start to within the game window
       Cursor.visible = false;//hides the cursor from view
    }

    public void CursorEnabled() //this method is called by the "Gallery guide management" fungus block
    {
      Cursor.visible = true;//show the cursor
    }

    public void CursorDisabled() //this method is called by the Gallery Guide close button
    {
      Cursor.visible = false;//hide the cursor
    }

    void Update()
    {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//get the X mouse movement input 
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//get the Y mouse movement input

      xRotation -= mouseY; //Decrease X rotation every frame based on mouseY
      xRotation = Mathf.Clamp(xRotation,-90f,90f); //clamp the X rotation so the player can't over- rotate and flip over to look behind themselves

      transform.localRotation = Quaternion.Euler(xRotation,0f,0f); // apply this rotation
      playerBody.Rotate(Vector3.up * mouseX); //rotate the player body around the X axis according to mouseX 
    
    }
}