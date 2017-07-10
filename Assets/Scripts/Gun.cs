using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	GameObject gunTip;


	// Use this for initialization
	void Start () {
		gunTip = GameObject.Find ("GunTip");
		if (gunTip == null)
			Debug.LogWarning ("No gunTip!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
