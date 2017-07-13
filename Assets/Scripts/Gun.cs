using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	GameObject gunTip;
	GameObject hitParticle;
	GameObject shellsParticle;
	GameObject shellsSpawn;
	GameObject muzzleFlash;
	GameObject bullet;
	float damage;
	float range;
	float accuracy;
	LayerMask layerMask = 1 << 8;
	bool isMainPlayersGun = false;


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

		if (transform.parent.gameObject.GetComponent<PlayerControls> () != null)
			isMainPlayersGun = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		Debug.DrawRay (gunTip.transform.position, gunTip.transform.forward * range, Color.red);
	}

	public void Fire()
	{
		RaycastHit hit;
		Vector3 dir = gunTip.transform.forward;
		dir.x += Random.Range (-accuracy, accuracy);
		dir.z += Random.Range (-accuracy, accuracy);

		Ray ray = new Ray (gunTip.transform.position, dir);

		GameObject bulletInstance;
		Vector3 targetPos;

		if (Physics.Raycast (ray, out hit, range, layerMask))
		{
			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health != null)
			{
				health.TakeDamage (damage, isMainPlayersGun);
				GameObject hitEffect;

				if (damage >= 20)
					hitEffect = health.hitEffectBig;
				else
					hitEffect = health.hitEffect;

				if (hitEffect != null)
					Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));
			}

			targetPos = hit.point;

		} else
		{
			Instantiate (hitParticle, ray.GetPoint (range), Quaternion.identity);
			targetPos = ray.GetPoint (range);
		}

		Instantiate (shellsParticle, shellsSpawn.transform.position, transform.rotation);
		Instantiate (muzzleFlash, gunTip.transform.position, Quaternion.identity);

		bulletInstance = Instantiate (bullet, gunTip.transform.position, gunTip.transform.rotation);
//		bulletInstance.GetComponent<Bullet> ().SetDir (dir);
		bulletInstance.GetComponent<Bullet> ().SetPos (targetPos);
	}

	public void SetProperties(float damage, float range, float accuracy, GameObject hitParticle, GameObject shellsParticle, 
		GameObject muzzleFlash, GameObject bullet)
	{
		this.damage = damage;
		this.range = range;
		this.accuracy = (100 - accuracy)/100;

		this.hitParticle = hitParticle;
		this.shellsParticle = shellsParticle;
		this.muzzleFlash = muzzleFlash;
		this.bullet = bullet;
	}
		
}
