using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	[Header("Attack")]
	public float damage = 10f;
	public float attackInterval = 1f;
	public float searchInterval = 3f;
	float searchTimer;
	float attackTimer;
	protected GameObject attackTarget = null;


	// Use this for initialization
	void Start () 
	{
		searchTimer = 0f;
		attackTimer = attackInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
		searchTimer -= Time.deltaTime;
		attackTimer -= Time.deltaTime;

		if (searchTimer <= 0)
		{
			attackTarget = GetClosestTarget ();
			searchTimer = searchInterval;
		}

		if (attackTimer <= 0)
		{
			if (attackTarget != null)
			{
				Attack (attackTarget);
				attackTimer = attackInterval;
			}
		}
	}

	protected virtual void Attack(GameObject target)
	{

	}

	protected virtual GameObject GetClosestTarget()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		GameObject closestTarget = null;
		float closestDistance = -1;

		foreach (GameObject target in targets)
		{
			float distance = Vector3.Distance (target.transform.position, transform.position);
			if (distance < closestDistance || closestDistance == -1)
			{
				closestDistance = distance;
				closestTarget = target;
			}
		}

		return closestTarget;
	}
}
