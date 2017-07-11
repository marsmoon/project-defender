using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public float rotatingSpeed = 1.75f;
	[Header("Attack")]
	public float damage = 10f;
	public float attackInterval = 1f;
	public float searchInterval = 3f;
	public float attackRange = 1f;
	[Range(0f, 100f)] public float accuracy = 75f;
	[Header("Effects")]
	public GameObject hitParticle;
	public GameObject hitDirtParticle;
	public GameObject shellsParticle;
	protected GameObject attackTarget = null;
	protected string enemyTag = "";
	float searchTimer;
	float attackTimer;
	Gun gun;


	// Use this for initialization
	protected virtual void Start () 
	{
		searchTimer = 0f;
		attackTimer = 0f;

		if (gameObject.tag == "Player")
			enemyTag = "Enemy";
		else if (gameObject.tag == "Enemy")
			enemyTag = "Player";

		gun = gameObject.GetComponentInChildren<Gun> ();

		if (gun != null)
			gun.SetProperties(damage, attackRange, accuracy, hitParticle, hitDirtParticle, shellsParticle);
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		searchTimer -= Time.deltaTime;
		attackTimer -= Time.deltaTime;

		if (searchTimer <= 0)
		{
			attackTarget = GetClosestTarget ();
			if (attackTarget != null)
				SetGunRotation (attackTarget.transform.position);
			
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

	protected virtual void Attack(GameObject target)
	{
//		SetGunRotation (target.transform.position);
		gun.Fire();
	}

	// sets the current gun rotation based on the current mouse position
	private void SetGunRotation(Vector3 targetPos){
		Vector3 diff = targetPos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.Euler(90, 0f, rot_z - 90), Time.deltaTime * rotatingSpeed);
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
