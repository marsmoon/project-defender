using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuilding : AI {

	public float rotatingSpeed = 1.75f;
	bool isRotated;
	GunVariables gunVariables;
	Gun gun;


	// Use this for initialization
	protected virtual void Start () 
	{
		base.Start ();
		isRotated = false;

		gun = gameObject.GetComponentInChildren<Gun> ();
		if (gun == null)
			Debug.LogWarning ("no gun!");

		gunVariables = gameObject.GetComponent<GunVariables> ();
		if (gunVariables == null)
			Debug.LogWarning ("no gunVariables!");

		gun.SetProperties(damage, range, gunVariables.accuracy, gunVariables.hitParticle, gunVariables.shellsParticle, 
			gunVariables.muzzleFlash);
	}

	// Update is called once per frame
	protected virtual void Update () 
	{
		base.Update ();

		if (attackTarget != null)
			isRotated = IsDoneRotating (attackTarget.transform.position);

		if (searchTimer <= 0)
		{
			attackTarget = GetClosestTarget ();
			searchTimer = searchInterval;
		}

		if (attackTimer <= 0)
		{
			if (attackTarget != null && isRotated)
			{
				Attack (attackTarget);
				attackTimer = attackInterval;

			} else if (attackTarget == null || !isRotated)
			{
				searchTimer = 0f;
			}
		}

		if (attackTarget != null)
			SetRotation (attackTarget.transform.position);
	}

	protected virtual void Attack(GameObject target)
	{
		gun.Fire();
	}

	// sets the current gun rotation based on the current mouse position
	protected void SetRotation(Vector3 targetPos){
		Vector3 diff = targetPos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.Euler(90, 0f, rot_z - 90), Time.deltaTime * rotatingSpeed);
	}

	bool IsDoneRotating(Vector3 targetPos)
	{
		Vector3 diff = targetPos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;

//		Debug.Log ();

//		if (transform.rotation == Quaternion.Euler (90, 0f, rot_z - 90))
		if (Mathf.Abs(transform.rotation.eulerAngles.y - Quaternion.Euler(90, 0f, rot_z - 90).eulerAngles.y) <= 15f)
			return true;
		else
			return false;
	}

}
