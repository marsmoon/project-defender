using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Attack(float damage)
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
		{
			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
				health.TakeDamage (damage);
		}
	}
}
