using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHumanoid : AI {

	NavMeshAgent agent;
	Vector3 lastDestination = Vector3.zero;
	Weapon[] weapons;
	GameObject mainPlayerCharacter;
	NavMeshPath path;


	// Use this for initialization
	protected override void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
		base.Start ();
		mainPlayerCharacter = GameObject.FindObjectOfType<PlayerControls> ().gameObject;
		path = new NavMeshPath();
	}

	protected override void Update()
	{
		base.Update ();

		if (searchTimer <= 0)
		{
			if (agent.CalculatePath (mainPlayerCharacter.transform.position, path) && path.status != NavMeshPathStatus.PathPartial)
			{
				attackTarget = mainPlayerCharacter;

			} else
			{
				attackTarget = GetClosestTarget ();
			}
			
			searchTimer = searchInterval;
		}

		if (attackTimer <= 0)
		{
			if (attackTarget != null)
			{
				Attack (attackTarget);
				attackTimer = attackInterval;

			} else
			{
				searchTimer = 0f;
			}
		}
	}

	protected override void Attack(GameObject target)
	{
		Collider col = target.GetComponent<Collider> ();
		if (col == null)
			col = target.GetComponentInChildren<Collider> ();
		
		Bounds bounds = col.bounds;
		Vector3 targetPos = bounds.ClosestPoint (transform.position);

		float dist = Vector3.Distance (targetPos, transform.position);
		if (dist <= range)
		{
			transform.LookAt (targetPos);

//			if (AreBothArmsHitting ())
			if (IsAnyArmHitting ())
			{
				foreach (Weapon wep in weapons)
				{
					wep.Attack (damage, range);
				}

			} else
			{
				Go (targetPos);
			}

		} else
		{
			Go (targetPos);
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
			if (!wep.IsHittingEnemy(range))
				return false;
		}

		return true;
	}

	bool IsAnyArmHitting()
	{
		foreach (Weapon wep in weapons)
		{
			if (wep.IsHittingEnemy(range))
				return true;
		}

		return false;
	}
		
}
