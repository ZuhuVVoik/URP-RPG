using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
	PlayerInput input;
	AgentMovement movement;

	private void OnEnable()
	{
		input = GetComponent<PlayerInput>();
		movement = GetComponent<AgentMovement>();
		input.OnMovementDirectionInput += movement.HandleMovementDirection;
		input.OnMovementInput += movement.HandleMovement;
		input.OnRun += movement.HandelRunWalk;
	}

	private void OnDisable()
	{
		input.OnMovementDirectionInput -= movement.HandleMovementDirection;
		input.OnMovementInput -= movement.HandleMovement;
		input.OnRun -= movement.HandelRunWalk;
	}
}
