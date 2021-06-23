using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RuleBasedSystem : MonoBehaviour
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
	private float RBStimer;
	private bool laden;
	private bool complete;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		navmesh = GetComponent<NavMeshAgent>();
		pickupCount = 0;
		detected = false;
		timer = wanderTimer;
		complete = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (detected && !laden)
		{
			find();
		}

		if (!detected && !laden)
		{
			wander();
		}

		if (!detected && laden)
		{
			deposit();
		}

		RBStimer += Time.deltaTime;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Pickup")

		{
			pickupCount++;
			other.gameObject.SetActive(false);
			detected = false;
			laden = true;
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
			laden = false;
		}

		if (other.gameObject.tag == "Dropzone" && pickupCount == 3 && !complete)
		{
			Debug.Log("RBS Timer");
			Debug.Log(RBStimer);
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
		Vector3 randDirection = Random.insideUnitSphere * dist;

		randDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

		return navHit.position;
	}
}