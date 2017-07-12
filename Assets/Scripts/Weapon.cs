using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	GameObject hitParticle;


	public void Attack(float damage, float range)
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -transform.up, out hit, range))
		{
			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
			{
				health.TakeDamage (damage);
				if (health.hitEffect != null)
					Instantiate (health.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
			}
		}
	}

	public bool IsHittingEnemy(float range)
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, range) && hit.collider.gameObject.tag == "Player")
			return true;
		 else
			return false;
	}

	public void SetHitParticle(GameObject particle)
	{
		hitParticle = particle;
	}
}
