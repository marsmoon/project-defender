using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour {

	public float destroyAfter = 0f;


	// Use this for initialization
	void Start () {
		Destroy (gameObject, destroyAfter);
	}

}
