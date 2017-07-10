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
	protected string enemyTag = "";


	// Use this for initialization
	protected virtual void Start () 
	{
		searchTimer = 0f;
		attackTimer = attackInterval;

		if (gameObject.tag == "Player")
			enemyTag = "Enemy";
		else if (gameObject.tag == "Enemy")
			enemyTag = "Player";
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
		SetGunRotation (target.transform.position);
	}

	// sets the current gun rotation based on the current mouse position
	private void SetGunRotation(Vector3 targetPos){
		Vector3 diff = targetPos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(90, 0f, rot_z - 90);
	}

	protected virtual GameObject GetClosestTarget()
	{
		if (enemyTag == "")
			return null;
		
		GameObject[] targets = GameObject.FindGameObjectsWithTag (enemyTag);
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
