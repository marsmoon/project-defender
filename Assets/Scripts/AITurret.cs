using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : AI {

	private SphereCollider collider;

	// Use this for initialization
	protected override void Start () {
		this.collider = GetComponent<SphereCollider>();
		base.Start();
	}
	
	// Update is called once per frame

	void SetGunRotation(Vector3 targetPos){
		Vector3 diff = targetPos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.Euler(90, 0f, rot_z - 90), Time.deltaTime * speed);
	}

	protected override GameObject GetClosestTarget(){
		if (enemyTag == "")
			return null;

		Collider[] targets = Physics.OverlapSphere (collider.center, collider.radius);
		Collider closestTarget = null;
		float closestDistance = -1;

		foreach (Collider target in targets)
		{
			float distance = Vector3.Distance (target.transform.position, transform.position);
			if (distance < closestDistance || closestDistance == -1)
			{
				closestDistance = distance;
				closestTarget = target;
			}
		}

		return closestTarget.gameObject;
	}
}
