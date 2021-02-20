using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	CharacterController controller;
	public Animator animator;
	public float rotationSpeed, movementSpeed, gravity = 20;
	public float runSpeed = 12f;
	Vector3 movementVector = Vector3.zero;
	private float desiredRotationAngle = 0;
	Vector3 directionVector = Vector3.forward;
	Vector3 leftRightDirection;

	Vector2 lastMove;

	bool isWalking = false;
	bool isRunning = false;
	bool isCrouching = false;
	bool turnRight = false;
	bool turnLeft = false;

	public bool freezed = false;
	public bool interracting = false;

	private float currentSpeed = 0f;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	public void HandleMovement(Vector2 input)
	{
		if (controller.isGrounded)
		{
			lastMove = input;

			if (input.y > 0)
			{
				directionVector = transform.forward;
			}
			else
			{
				if(input.y < 0)
                {
					directionVector = -transform.forward;
                }
                else
                {
					directionVector = Vector3.zero;
				}
			}


			//Уже вектор не ноль, должны слаживать
			if(input.x > 0)
            {
				directionVector += transform.right;
				leftRightDirection = Vector3.right;
			}
            else
            {
				if(input.x < 0)
                {
					directionVector += -transform.right;
					leftRightDirection = Vector3.left;
				}
                else
                {
					leftRightDirection = Vector3.zero;
                }
            }

			movementVector = currentSpeed * directionVector.normalized;
		}
	}
	public void HandelRunWalk(bool value)
    {
        if (value)
        {
			currentSpeed = runSpeed;
        }
        else
        {
			currentSpeed = movementSpeed; 
        }
    }
	public void HandleMovementDirection(Vector3 direction)
	{
		desiredRotationAngle = Vector3.Angle(transform.forward, direction);
		var crossProduct = Vector3.Cross(transform.forward, direction).y;
		if (crossProduct < 0)
		{
			desiredRotationAngle *= -1;
		}
	}

	private void RotateAgent()
	{
        if ((leftRightDirection == Vector3.right && lastMove.y < 0) || (leftRightDirection == Vector3.left && lastMove.y > 0))
        {
			desiredRotationAngle -= 30;
        }
		if((leftRightDirection == Vector3.right && lastMove.y > 0) || (leftRightDirection == Vector3.left && lastMove.y < 0))
        {
			desiredRotationAngle += 30;
        }

		transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
	}

	

	private void Update()
	{
		if (controller.isGrounded)
		{
			if (movementVector.magnitude > 0)
			{
				RotateAgent();
			}
		}
		movementVector.y -= gravity;
        if (!freezed)
        {
			controller.Move(movementVector * Time.deltaTime);
			Animate();
		}
	}
	public void Animate()
    {
        if (lastMove.y != 0)
        {
			animator.SetBool("IsWalking", true);
			if(currentSpeed == runSpeed)
            {
				animator.SetBool("IsRunning", true);
			}
		}
        else
        {
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsRunning", false);
        }
    }

	public void OnInterraction(InteractableObject interactableObject)
	{

	}
}
