using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private const float SPEED = 4f;
    private float moveX, moveY = 0f, moveZ;

    [SerializeField]
    private CharacterController controller;
    private Vector3 moveDir;
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void MovingWithTransformPosition()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //transform.position = new Vector3(tranform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void MovingWithTransformTranslate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);
        }
    }

    private void MovingWithVelocity()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(moveX, moveY, moveZ);
    }

    private void MovingWithCharController()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveY = 0.15f;
            }
            else
            {
                moveY = 0f;
            }
        }
        else
        {
            //moveY -= 1f = Time.fixedDeltaTime;
        }

        moveDir = new Vector3(moveX * Time.fixedDeltaTime, moveY, moveZ * Time.fixedDeltaTime);
        controller.Move(moveDir * SPEED);
    }

}
