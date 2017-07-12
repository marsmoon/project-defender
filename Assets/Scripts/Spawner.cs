﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


	public void Spawn(GameObject prefab)
	{
		Instantiate (prefab, new Vector3 (transform.position.x, 0f, transform.position.z), Quaternion.identity);
	}

}
