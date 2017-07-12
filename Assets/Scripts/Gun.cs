using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	GameObject gunTip;
	GameObject hitParticle;
	GameObject shellsParticle;
	GameObject shellsSpawn;
	GameObject muzzleFlash;
	float damage;
	float range;
	float accuracy;
	LayerMask layerMask = 1 << 8;


	// Use this for initialization
	void Start () {
		layerMask = ~layerMask;

		gunTip = transform.Find ("GunTip").gameObject;
		if (gunTip == null)
			Debug.LogWarning ("No gunTip!");
		
		shellsSpawn = transform.Find ("Shells").gameObject;
		if (shellsSpawn == null)
			Debug.LogWarning ("No Shells");

		shellsSpawn.transform.localPosition = new Vector3(-0.65f, -0.2f, -1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.DrawRay (gunTip.transform.position, gunTip.transform.forward * range, Color.red);
	}

	public void Fire()
	{
		RaycastHit hit;
		Vector3 dir = gunTip.transform.forward;
		dir.x += Random.Range (-accuracy, accuracy);
		dir.z += Random.Range (-accuracy, accuracy);

		Ray ray = new Ray (gunTip.transform.position, dir);

		if (Physics.Raycast (ray, out hit, range, layerMask))
		{
			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
			{
				health.TakeDamage (damage);
				GameObject hitEffect;

				if (damage >= 20)
					hitEffect = health.hitEffectBig;
				else
					hitEffect = health.hitEffect;

				if (hitEffect != null)
					Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));
			}

		} else
		{
			Instantiate (hitParticle, ray.GetPoint (range), Quaternion.identity);
		}

		Instantiate (shellsParticle, shellsSpawn.transform.position, transform.rotation);
		Instantiate (muzzleFlash, gunTip.transform.position, Quaternion.identity);
	}

	public void SetProperties(float damage, float range, float accuracy, GameObject hitParticle, GameObject shellsParticle, 
		GameObject muzzleFlash)
	{
		this.damage = damage;
		this.range = range;
		this.accuracy = (100 - accuracy)/100;

		this.hitParticle = hitParticle;
		this.shellsParticle = shellsParticle;
		this.muzzleFlash = muzzleFlash;
	}
		
}
