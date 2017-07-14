using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DolarEffect : MonoBehaviour {

	TextMesh textMesh;
	float alpha;
	float green;

	void Start()
	{
		textMesh = GetComponent<TextMesh> ();
		alpha = textMesh.color.a;
		green = textMesh.color.g;
	}

	// Update is called once per frame
	void Update () 
	{
		transform.Translate (transform.up * 10f * Time.deltaTime, Space.World);
		transform.localScale = new Vector3 (transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z + 0.01f);
		alpha = Mathf.Lerp (1, 0, 20f * Time.deltaTime);
		green = Mathf.Lerp (green, 0, 1f * Time.deltaTime);
		textMesh.color = new Color (textMesh.color.r, green, textMesh.color.b, alpha);
	}
}
