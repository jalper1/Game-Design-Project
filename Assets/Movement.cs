 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    float horMove = 0f;
    float vertMove = 0f;
    public float runSpeed = 40f;

    bool crouch = false;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        vertMove = Input.GetAxisRaw("Vertical") * runSpeed;
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        controller.MoveHor(horMove * Time.fixedDeltaTime, crouch, false);
        controller.MoveVert(vertMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
