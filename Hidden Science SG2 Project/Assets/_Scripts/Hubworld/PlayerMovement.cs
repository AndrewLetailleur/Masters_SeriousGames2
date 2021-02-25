//Danica's script (based on Brackeys tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&list=LL&index=7)
//This is for player movement

//***will be changing this later

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //check whether player is currently colliding with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);

        //reset the velocity when player is grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //move player with WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //jump when space key pressed
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("space");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(move*speed*Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
