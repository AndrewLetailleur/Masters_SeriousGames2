//Danica's script (based on Brackeys tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&list=LL&index=7)
//This is for player movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        //check whether player is currently colliding with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);

        //reset the velocity when player is grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;//(note: set this to -2 instead of 0f so that the player is definitely on the ground)
        }

        //detect player input with WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //calculate the direction the player will move based on input and the direction they are facing
        Vector3 move = transform.right * x + transform.forward * z;

        //move the player via the character controller
        controller.Move(move*speed*Time.deltaTime);

        //increase player velocity on the y axis by gravity
        velocity.y += gravity * Time.deltaTime;

        //move the player based on the velocity
        controller.Move(velocity * Time.deltaTime);

    }

}
