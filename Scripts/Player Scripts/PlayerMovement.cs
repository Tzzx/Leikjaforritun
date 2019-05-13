﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController Character_Controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float Jump_Force = 10f;
    private float vertical_Velocity;

    private void Awake()
    {
        Character_Controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }


    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                      Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        Character_Controller.Move(move_Direction);


    } //hreyfa spilara


    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;

        //hoppa
        PlayerJump();

        move_Direction.y = vertical_Velocity * Time.deltaTime;
            // gerið þetta smooth
        
    } // gerið þýngdarafl


    void PlayerJump()
    {
        if (Character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            vertical_Velocity = Jump_Force;
        }


    }









}