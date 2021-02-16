// Becca's script for virus interactions in almeida mimigame
// Raycast script adapted from semester 1 lectures and previous unity project
using UnityEngine;


public class VirusGame : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo); //this tells us where the mouse has hit on the screen
            //Debug.Log("left click");

            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Virus")
                {
                    Debug.Log("viruses!"); // prints out when click on virus                   
                    VirusCount.instance.virusCount += 1; // add 1 to virus count tally
                }

                else if (hitInfo.transform.gameObject.tag == "CellDebris")
                {
                    Debug.Log("debris hit!"); // prints out when click debris
                    DebrisCount.instance.debrisCount += 1; // add 1 to debris count tally
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
        
    }
}
