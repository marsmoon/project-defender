using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHumanoid : AI {

	public float attackRange = 1f;
	[Header("Movement")]
	public float movementSpeed = 10f;
	NavMeshAgent agent;
	Vector3 lastDestination = Vector3.zero;
	Weapon[] weapons;


	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
	}
	
	// Update is called once per frame
//	void Update () 
//	{
//
//	}

	protected override void Attack(GameObject target)
	{
		float dist = Vector3.Distance (target.transform.position, transform.position);
		if (dist <= attackRange)
		{
			foreach (Weapon wep in weapons)
			{
				wep.Attack (damage);
			}

		} else
		{
			Go (target.transform.position);
		}
	}

	void Go(Vector3 position)
	{
		if (agent.isStopped || position != lastDestination)
		{
			agent.SetDestination (position);
			lastDestination = position;
		}
	}


}
