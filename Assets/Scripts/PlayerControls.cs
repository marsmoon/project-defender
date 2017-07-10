using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	GameObject gun;


	// Use this for initialization
	void Start () 
	{
		gun = GameObject.Find ("Gun");
		if (gun == null)
			Debug.LogWarning ("No gun!");
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
