using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	float range = 0.5f;

	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, -transform.up * range, Color.red);
	}

	public void Attack(float damage)
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -transform.up, out hit, range))
		{
			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
				health.TakeDamage (damage);
		}
	}

	public bool IsHittingEnemy()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, range) && hit.collider.gameObject.tag == "Player")
			return true;
		 else
			return false;
	}
}
