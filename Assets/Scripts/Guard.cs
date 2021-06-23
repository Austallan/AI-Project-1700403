using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Guard : MonoBehaviour
{

	// Enum to describe AI state. Can be expanded
	protected enum State { Search = 0, Locate = 1, Laden = 2, Wait = 3 };

	// State all the Enemies start in
	protected State aiState = State.Search;

	protected void ChangeState(State newState)
	{
		// End the current state
		EndCurrentState(aiState);

		// Change the state
		aiState = newState;

		// Call the start of the new state
		StartNewState(aiState);
	}

	protected bool isState(State checkState)
	{
		return (aiState == checkState);
	}

	protected virtual void EndCurrentState(State currentState)
	{
		// Do nothing - needs to be overwritten
	}

	protected virtual void StartNewState(State currentState)
	{
		// Do nothing - needs to be overwritten
	}
}

/*
public class Guard : FiniteStateMachine
{
	public GameObject pickup;

	private NavMeshAgent navmesh;

	// Start is called before the first frame update
	void Start()
	{
		navmesh = GetComponent<NavMeshAgent>();
	}

	// Called at set interval each time. Good for physics
	void FixedUpdate()
	{
		navmesh.destination = pickup.transform.position;

		// Extra movement details for specific states
		switch (aiState)
		{
			case State.Laden:
				
				break;
			case State.Search:
				
				break;
			default:
				break;
		}
	}
	

	// Called once when we swap to a new state
	protected override void StartNewState(State currentState)
	{
		// Depending on the state, use the appropriate banner sprite
		switch (currentState)
		{
			case State.Search:				
				break;
			case State.Locate:
				break;
			case State.Laden:				
				break;
			case State.Wait:		
				break;
			default:
				break;
		}
	}
}
*/