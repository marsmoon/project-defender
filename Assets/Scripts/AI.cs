using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	[Header("Attack")]
	public float damage = 10f;
	public float range = 1f;
	public float attackInterval = 1f;
	public float searchInterval = 3f;
	protected GameObject attackTarget = null;
	protected string enemyTag = "";
	protected float searchTimer;
	protected float attackTimer;


	// Use this for initialization
	protected virtual void Start () 
	{
		searchTimer = searchInterval;
		attackTimer = 0f;

		if (gameObject.tag == "Player")
			enemyTag = "Enemy";
		else if (gameObject.tag == "Enemy")
			enemyTag = "Player";

	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		searchTimer -= Time.deltaTime;
		attackTimer -= Time.deltaTime;
	}

	protected virtual void Attack(GameObject target)
	{

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
