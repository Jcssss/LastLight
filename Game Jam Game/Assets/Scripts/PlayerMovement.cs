using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool holdingJump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // A = -1, D = 1

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButton("Jump")) {
            holdingJump = true;
        } else {
            holdingJump = false;
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

    }

    void FixedUpdate () {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, holdingJump);
        jump = false;
    }
}
