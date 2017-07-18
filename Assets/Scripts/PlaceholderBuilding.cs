using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderBuilding : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Material buildingGood;
	Material buildingBad;
	Material spritesDefault;
	Rigidbody rigidbody;
	BoxCollider col;
	float rotateAmount = 10f;
	bool canBePlaced;


	// Use this for initialization
	void Start () 
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		buildingGood = Resources.Load<Material> ("building-good");
		buildingBad = Resources.Load<Material> ("building-bad");
		spritesDefault = Resources.Load<Material> ("sprites-default");

		if (spriteRenderer != null)
			spriteRenderer.material = buildingGood;

		col = gameObject.GetComponent<BoxCollider> ();

		if (col == null)
		{
			col = gameObject.AddComponent<BoxCollider> ();
			BoxCollider childCol = gameObject.GetComponentInChildren<BoxCollider> ();
			col.center = childCol.center;
			col.size = childCol.size;

		} else
		{
			col.isTrigger = true;
		}

		rigidbody = gameObject.AddComponent<Rigidbody> ();
		rigidbody.isKinematic = true;
		rigidbody.useGravity = false;

		canBePlaced = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.y = 0;
		transform.position = mousePos;

		if (Input.GetButtonDown ("Fire1") && canBePlaced)
			PlaceBuilding ();

		if (Input.GetAxis ("Mouse ScrollWheel") > 0f)
		{
			transform.Rotate (new Vector3 (0f, rotateAmount, 0f), Space.World);

		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0f)
		{
			transform.Rotate (new Vector3 (0f, -rotateAmount, 0f), Space.World);
		}
	}
		
	void PlaceBuilding()
	{
		if (spriteRenderer != null)
			spriteRenderer.material = spritesDefault;

		col.isTrigger = false;
		Component.Destroy (rigidbody);
		Component.Destroy (this);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "Terrain")
			return;
		
		if (spriteRenderer != null)
			spriteRenderer.material = buildingBad;

		canBePlaced = false;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Terrain")
			return;

		if (spriteRenderer != null)
			spriteRenderer.material = buildingGood;

		canBePlaced = true;
	}
}
