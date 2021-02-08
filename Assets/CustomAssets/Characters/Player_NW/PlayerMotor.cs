using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public Animator animator;


    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    public float moveSpeed = 6f;

    public float gravity = 1f;


    bool isWalking = false;
    bool isRunning = false;
    bool isCrouching = false;
    bool turnRight = false;
    bool turnLeft = false;

    public bool freezed = false;
    public bool interracting = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down.normalized * Time.deltaTime * gravity);
        }

        if (!freezed)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            ChangeSpeed(vertical, horizontal);

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Deg2Rad + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 mRot = new Vector3();

                if (vertical == 1)
                {
                    if (horizontal == 1)
                    {
                        mRot = (Vector3.right + Vector3.forward).normalized;
                    }
                    if (horizontal == -1)
                    {
                        mRot = (Vector3.left + Vector3.forward).normalized;
                    }
                    if (horizontal == 0)
                    {
                        mRot = Vector3.forward;
                    }
                }
                if (vertical == -1)
                {
                    if (horizontal == 1)
                    {
                        mRot = (Vector3.right + Vector3.back).normalized;
                    }
                    if (horizontal == -1)
                    {
                        mRot = (Vector3.left + Vector3.back).normalized;
                    }
                    if (horizontal == 0)
                    {
                        mRot = Vector3.back;
                    }
                }
                if (vertical == 0)
                {
                    if (horizontal == 1)
                    {
                        mRot = Vector3.right;
                    }
                    if (horizontal == -1)
                    {
                        mRot = Vector3.left;
                    }
                }


                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * mRot;
                controller.Move(moveDir * Time.deltaTime * moveSpeed);
            }
        }
        Animate();
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        ChangeSpeed(vertical,horizontal);

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        Debug.Log(moveDirection);

        if (moveDirection.magnitude >= 0.1f)
        {
               

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Deg2Rad + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            ChangeDiagonalRotation(vertical, horizontal, targetAngle);

            Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            if (vertical == 1 || vertical == -1)
            {
                if (horizontal == 0)
                {
                    if (vertical == 1)
                    {
                        controller.Move(move.normalized * moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        controller.Move(-move.normalized * moveSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    if (vertical == 1)
                    {
                        if (horizontal == 1)
                        {
                            controller.Move((transform.right + move.normalized) * moveSpeed * Time.deltaTime);
                        }
                        if (horizontal == -1)
                        {
                            controller.Move((-transform.right + move.normalized) * moveSpeed * Time.deltaTime);
                        }
                    }
                    if (vertical == -1)
                    {
                        if (horizontal == 1)
                        {
                            controller.Move((transform.right + -move.normalized) * moveSpeed * Time.deltaTime);
                        }
                        if (horizontal == -1)
                        {
                            controller.Move((-transform.right + -move.normalized) * moveSpeed * Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                if (horizontal == 1)
                {
                    controller.Move((transform.right + move.normalized) * Time.deltaTime);
                }
                if (horizontal == -1)
                {
                    controller.Move((-transform.right + move.normalized) * Time.deltaTime);
                }
            }
        }


        Animate();


    }

    private void ChangeDiagonalRotation(float verticalMovement, float horizontalMovement, float moveAngle)
    {
        if(verticalMovement != 0)
        {
            if (turnRight)
            {
                transform.rotation = Quaternion.Euler(0f, moveAngle - 30, 0f);
            }
            if (turnLeft)
            {
                transform.rotation = Quaternion.Euler(0f, moveAngle + 30, 0f);
            }
            if (!turnLeft && !turnRight)
            {
                transform.rotation = Quaternion.Euler(0f, moveAngle, 0f);
            }
        }
        else
        {
            
        }
    }

    private void ChangeSpeed(float verticalMovement, float horizontalMovement)
    {
        if(horizontalMovement == 1)
        {
            turnLeft = true;
            turnRight = false;
        }
        if(horizontalMovement == -1)
        {
            turnRight = true;
            turnLeft = false;
        }
        if(horizontalMovement == 0)
        {
            turnRight = false;
            turnLeft = false;
        }


        if (verticalMovement == 1 || verticalMovement == -1)
        {
            if (Input.GetButton("Run") && verticalMovement == 1)
            {
                moveSpeed = runSpeed;
                isRunning = true;
            }
            isWalking = true;
        }
        else
        {
            moveSpeed = walkSpeed;
            isRunning = false;
            isWalking = true;
        }


        if (verticalMovement == 0 && horizontalMovement == 0)
        {
            isWalking = false;
        }

        
    }

    private void Animate()
    {
        if (isRunning)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (isWalking)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (turnRight)
        {
            animator.SetBool("RunningRight", true);
        }
        else
        {
            animator.SetBool("RunningRight", false);
        }


        /*
        if (isCrouching)
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
        */
    }

    public void OnInterraction(InteractableObject interactableObject)
    {

    }
}
