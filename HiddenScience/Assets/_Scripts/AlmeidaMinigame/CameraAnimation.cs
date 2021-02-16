// Becca's animation script for camera in virus game
// Adapted from semester 1 project and lectures
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    // Set animation variable
    Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // On click of lets go button the animation will start
    public void CameraMoving()
    {
        myAnim.SetBool("DoAnimation", true);
    }
}
