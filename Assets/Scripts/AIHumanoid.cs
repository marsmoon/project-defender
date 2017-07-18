using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHumanoid : AI {

	public int minMoneyDropAmount = 5;
	public int maxMoneyDropAmount = 10;
	NavMeshAgent agent;
	Vector3 lastDestination = Vector3.zero;
	Weapon[] weapons;
	GameObject mainPlayerCharacter;
	NavMeshPath path;
	Personality personality;

	public enum Personality
	{
		dumb,
		smart
	};


	// Use this for initialization
	protected override void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
		base.Start ();
		mainPlayerCharacter = GameObject.FindObjectOfType<PlayerControls> ().gameObject;
		path = new NavMeshPath();
		personality = (Personality)Random.Range (0, 2);
	}

	protected override void Update()
	{
		base.Update ();

		if (searchTimer <= 0)
		{
			if (personality == Personality.smart && agent.CalculatePath (mainPlayerCharacter.transform.position, path) 
				&& path.status != NavMeshPathStatus.PathPartial)
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
		Bounds bounds;
		Vector3 targetPos;

		if (col == null)
			col = target.GetComponentInChildren<Collider> ();

		if (col == null)
		{
			targetPos = target.transform.position;

		} else
		{
			bounds = col.bounds;
			targetPos = bounds.ClosestPoint (transform.position);
		}
			

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

	public void DropMoney()
	{
		if (mainPlayerCharacter != null)
			mainPlayerCharacter.GetComponent<PlayerControls> ().GetThatMoney (Random.Range(minMoneyDropAmount, maxMoneyDropAmount), transform.position);
	}
		
}
