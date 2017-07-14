using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Vector3 targetPos;
	bool ready = false;

	
	// Update is called once per frame
	void Update () {
		if (ready)
		{
			transform.position = Vector3.Lerp (transform.position, targetPos, 20f * Time.deltaTime);
			if (transform.position == targetPos)
				Destroy (gameObject);
		}
	}

	public void SetPos(Vector3 pos)
	{
		targetPos = pos;
		ready = true;
	}
}
