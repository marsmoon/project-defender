using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : GunVariables {

	[Header("")]
	public float damage = 10f;
	public float fireRate = 5f;
	public float range = 1f;
	public GameObject[] buildings;
	float nextTimeToFire = 0f;
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

		gun.SetProperties(damage, range, accuracy, hitParticle, shellsParticle, muzzleEffects, bullet);
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetGunRotation (GetCurrentMousePos ());

		if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1 / fireRate;
			gun.Fire ();
		}


		int buildingToSpawn = 0;
		int cost = 0;

		if (Input.GetKeyDown (KeyCode.Alpha1))
			buildingToSpawn = 1;

		if (Input.GetKeyDown (KeyCode.Alpha2))
			buildingToSpawn = 2;

		if ((buildingToSpawn == 1 || buildingToSpawn == 2) && buildings != null && buildings.Length > 0)
		{
			if (buildingToSpawn == 1)
				cost = 10;
			else if (buildingToSpawn == 2)
				cost = 50;

			if (money - cost >= 0)
			{
				buildingToSpawn--;
				Vector3 spawnPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				GameObject building = Instantiate (buildings [buildingToSpawn], spawnPos, buildings [buildingToSpawn].transform.rotation);
				building.AddComponent<PlaceholderBuilding> ();
				money -= cost;
				uiManager.SetMoneyCounter (money);
			}
		}

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

	public void GetThatMoney(int amount, Vector3 dropPos)
	{
		money += amount;
		uiManager.SetMoneyCounter (money);
		uiManager.DisplayDolarEffect (dropPos);
	}

}
