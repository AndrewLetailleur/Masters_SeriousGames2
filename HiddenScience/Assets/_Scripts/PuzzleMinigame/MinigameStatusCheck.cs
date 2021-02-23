using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStatusCheck : MonoBehaviour
{
    public GameObject gamePanel, endPanel;

    // Update is called once per frame
    void Update()
    {
        //not super efficient. But it will do for now?
        if (GameObject.FindGameObjectsWithTag("Draggable") == null)
        {
            gamePanel.SetActive(false);
            endPanel.SetActive(true);
            this.enabled = false;//the "disable component" part
        }
    }
}
