using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : GunVariables {

	[Header("")]
	public float damage = 10f;
	public float range = 1f;
	int money = 0;
	Gun gun;
	UIManager uiManager;


	// Use this for initialization
	void Start () 
	{
		gun = gameObject.GetComponentInChildren<Gun>();
		if (gun == null)
			Debug.LogWarning ("No gun!");

		uiManager = GameObject.Find ("_GameManager_").GetComponent<UIManager> ();

		gun.SetProperties(damage, range, accuracy, hitParticle, shellsParticle, muzzleFlash, bullet);
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetGunRotation (GetCurrentMousePos ());

		if (Input.GetButtonDown ("Fire1"))
			gun.Fire ();
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

	public void GetThatMoney(int amount)
	{
		money += amount;
		uiManager.SetMoneyCounter (money);
	}

}
