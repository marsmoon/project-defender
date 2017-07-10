using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	GameObject gunTip;


	// Use this for initialization
	void Start () 
	{
		gunTip = GameObject.Find ("GunTip");
		if (gunTip == null)
			Debug.LogWarning ("No gunTip!");
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetGunRotation (GetCurrentMousePos ());
	}

	// sets the current gun rotation based on the current mouse position
 	private void SetGunRotation(Vector3 currentMousePos){
		Vector3 diff = currentMousePos - transform.position;
		diff.Normalize();
		// z and x because our axis are fucked
		float rot_z = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(90, 0f, rot_z - 90);
	}

	// gets the current mouse position 
	private Vector3 GetCurrentMousePos(){
		Vector3 currentPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return currentPos;
	}

}
