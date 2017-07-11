using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHumanoid : AI {

	[Header("Movement")]
	public float movementSpeed = 10f;
	NavMeshAgent agent;
	Vector3 lastDestination = Vector3.zero;
	Weapon[] weapons;


	// Use this for initialization
	protected override void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
		SetWeaponsProperties ();
		base.Start ();
	}

	protected override void Attack(GameObject target)
	{
		float dist = Vector3.Distance (target.transform.position, transform.position);
		transform.LookAt (target.transform.position);
		if (dist <= attackRange && AreBothArmsHitting())
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

	bool AreBothArmsHitting()
	{
		foreach (Weapon wep in weapons)
		{
			if (!wep.IsHittingEnemy())
				return false;
		}

		return true;
	}

	void SetWeaponsProperties()
	{
		foreach (Weapon wep in weapons)
		{
			wep.SetHitParticle (hitParticle);
		}
	}


}
