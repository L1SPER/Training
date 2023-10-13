using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight=3f;
    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] float groundDistance=0.4f ;

    Vector3 velocity;
    bool isGrounded;

    public InventoryObject inventory;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            inventory.Save();
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
        Movement();
    }

    private void Movement()
    {
        if (CheckGround() && velocity.y < 0f)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && CheckGround())
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckGround()
    {
        return isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items= new InventorySlot[20];
    }
}
