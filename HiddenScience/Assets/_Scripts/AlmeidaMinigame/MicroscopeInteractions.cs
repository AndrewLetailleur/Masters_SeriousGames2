// Becca's script for interaction in almeida mimigame, microscope interactions
// Raycast script adapted from semester 1 lectures and previous unity project
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicroscopeInteractions : MonoBehaviour
{


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo); //this tells us where the mouse has hit on the screen
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "EyePiece")
                {
                    Debug.Log("eyepiece!"); // prints out when click on microscope                   
                    SceneManager.LoadScene("2-2_JuneAlmeidaVirusGame");
                    
                }
            }
        }
    }
}
         