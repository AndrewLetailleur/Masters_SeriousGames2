using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortraitInteraction : MonoBehaviour
{
    GameManager gm;

    void Awake() 
    {
        gm = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Player")
        {
            Debug.Log("Player touched portrait");
            gm.MicroscopeScene();
        
        }
    }

}
