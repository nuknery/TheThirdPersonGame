using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator animator;

    private float angleY, dirZ, jumpForce = 6f, turnSpeed = 80f;
    private bool isGrounded;
    private Vector3 jumpDir;
    
    void FixedUpdate()
    {
        angleY = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * turnSpeed;
        dirZ = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0f, angleY, 0f));
    }

    private void Update()
    {
        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else
            {
                animator.SetTrigger("isLanded");
            }
            Move(dirZ, "isWalkForward", "isWalkBack");
            Sprint();
            Dodge();
        }
        else
        {
            MoveInAir();
        }
    }

    private void Jump()
    {
        animator.Play("Sword_Jump_Platformer_Start");
        animator.applyRootMotion = false;
        jumpDir = new Vector3(0f, jumpForce, dirZ * jumpForce / 2f);
        jumpDir = transform.TransformDirection(jumpDir);
        rb.AddForce(jumpDir, ForceMode.Impulse);
        isGrounded = false; 
    }

    private void Move(float dir, string paramName, string altParamName)
    {
      if(dir > 0)
        {
            animator.SetBool(paramName, true);
        }
        else if(dir < 0)              
        {
            animator.SetBool(altParamName, true);
        }
        else
        {
            animator.SetBool(paramName, false);
            animator.SetBool(altParamName, false);                                      
        }
    }

    private void Sprint()
    {
        animator.SetBool("isRun", Input.GetKey(KeyCode.LeftShift));
    }

    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Sword_Dodgle_Left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {      
            animator.Play("Sword_Dodge_Right");              
        }
    }

    private void MoveInAir()
    {
        if(new Vector2(rb.velocity.x, rb.velocity.z).magnitude < 1.1f)
        {
            jumpDir = new Vector3(0f, rb.velocity.y, dirZ);
            jumpDir = transform.TransformDirection(jumpDir);
            rb.velocity = jumpDir;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        animator.applyRootMotion = true;
    }
}
