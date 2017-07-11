using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	GameObject gunTip;
	GameObject hitParticle;
	GameObject hitDirtParticle;
	float damage;
	float range;
	LayerMask layerMask = 1 << 8;


	// Use this for initialization
	void Start () {
		layerMask = ~layerMask;
		gunTip = transform.Find ("GunTip").gameObject;
		if (gunTip == null)
			Debug.LogWarning ("No gunTip!");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.DrawRay (gunTip.transform.position, gunTip.transform.forward * range, Color.red);
	}

	public void Fire()
	{
		RaycastHit hit;
		Ray ray = new Ray (gunTip.transform.position, gunTip.transform.forward);

		if (Physics.Raycast (ray, out hit, range, layerMask))
		{
			Instantiate (hitParticle, hit.point, Quaternion.identity);

			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
				health.TakeDamage (damage);

		} else
		{
			Instantiate (hitDirtParticle, ray.GetPoint (range), Quaternion.identity);
		}
	}

	public void SetDamage(float amount)
	{
		damage = amount;
	}

	public void SetRange(float amount)
	{
		range = amount;
	}

	public void SetHitParticle(GameObject particle)
	{
		hitParticle = particle;
	}

	public void SetHitDirtParticle(GameObject particle)
	{
		hitDirtParticle = particle;
	}
}
