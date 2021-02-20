using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public Action<Vector2> OnMovementInput { get; set; }
	public Action<Vector3> OnMovementDirectionInput { get; set; }
	public Action<bool> OnRun { get; set; }

	private void Start()
	{
		//Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		GetMovementInput();
		GetMovementDirection();
		GetRunOrWalk();

	}
	private void GetRunOrWalk()
    {
        if (Input.GetButton("Run"))
        {
			OnRun?.Invoke(true);
        }
        else
        {
			OnRun?.Invoke(false);
        }
    }
	private void GetMovementDirection()
	{
		var cameraForewardDIrection = Camera.main.transform.forward;
		Debug.DrawRay(Camera.main.transform.position, cameraForewardDIrection * 10, Color.red);
		var directionToMoveIn = Vector3.Scale(cameraForewardDIrection, (Vector3.right + Vector3.forward));
		Debug.DrawRay(Camera.main.transform.position, directionToMoveIn * 10, Color.blue);
		OnMovementDirectionInput?.Invoke(directionToMoveIn.normalized);
	}

	private void GetMovementInput()
	{
		float vertical = Input.GetAxisRaw("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");
		OnMovementInput?.Invoke(new Vector2(horizontal,vertical));
	}
}
