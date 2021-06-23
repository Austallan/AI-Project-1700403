using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{
	public GameObject fsm;
	public Transform objectA;
	public Transform objectB;
	public Vector3 passMe;


	void Start()
	{
		//Make ObjectA's position match objectB
		objectA.position = objectB.position;

		//Now parent the object so it is always there
		objectA.parent = objectB;
	}

	// Update is called once per frame
	void Update()
    {
		
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			GameObject gHit = other.gameObject;
			Transform tHit = gHit.transform;
			passMe = new Vector3(tHit.position.x, tHit.position.y, tHit.position.z);

			fsm.GetComponent<FiniteStateMachine>().detected = true;
			fsm.GetComponent<FiniteStateMachine>().targetVector = passMe;
		}
	}
}
