using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{
	public GameObject pickup;
	public GameObject dropZone;
	public float wanderRadius;
	public float wanderTimer;
	public Vector3 targetVector;
	public Vector3 thereHasToBeABetterWay;
	public int pickupCount;
	public bool detected;

	private NavMeshAgent navmesh;
	private Rigidbody rb;
	private Transform target;
	private float timer;
	private float FSMtimer;
	private bool complete;

	protected enum STATES { Wander = 0, Find = 1, Deposit = 2 };
	

	protected STATES states = STATES.Wander;

	protected void ChangeState(STATES newState)
	{
		// Change the state
		states = newState;
	}


	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		navmesh = GetComponent<NavMeshAgent>();
		pickupCount = 0;
		detected = false;
		timer = wanderTimer;
		FSMtimer = 0;
		complete = false;
	}

    // Update is called once per frame
    void Update()
    {

		if (detected)
		{
			detected = false;
			ChangeState(STATES.Find);
		}

		switch (states)
		{
			case STATES.Wander:
				wander();
				break;

			case STATES.Find:
				find();
				break;

			case STATES.Deposit:
				deposit();
				break;

			default:
				break;
		}

		FSMtimer += Time.deltaTime;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Pickup")

		{
			pickupCount++;
			other.gameObject.SetActive(false);
			ChangeState(STATES.Deposit);
			GameObject gHit = dropZone.gameObject;
			Transform tHit = gHit.transform;
			thereHasToBeABetterWay = new Vector3(tHit.position.x, tHit.position.y, tHit.position.z);
			targetVector = thereHasToBeABetterWay; 
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Dropzone" && pickupCount < 3)
		{
			ChangeState(STATES.Wander);
		}

		if (other.gameObject.tag == "Dropzone" && pickupCount == 3 && !complete)
		{
			Debug.Log("FSM Timer");
			Debug.Log(FSMtimer);
			complete = true;
		}
	}

	void wander()
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer)
		{
			Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
			navmesh.SetDestination(newPos);
			timer = 0;
		}
	}

	void find()
	{
		navmesh.destination = targetVector;
	}

	void deposit()
	{
		navmesh.destination = targetVector;
	}

	Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		 Vector3 randDirection  = Random.insideUnitSphere* dist;

		 randDirection += origin;

		 NavMeshHit navHit;
   
		 NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
   
		 return navHit.position;
	}
}