using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHealth = 50f;
	float health;
	public GameObject hitEffect;
	AIHumanoid humanoidScript;


	// Use this for initialization
	void Start () 
	{
		health = maxHealth;	
		humanoidScript = gameObject.GetComponent<AIHumanoid> ();
	}
		
	public void TakeDamage(float amount, bool dealtByMainPlayerCharacter)
	{
		health -= amount;

		if (health <= 0)
			Die (dealtByMainPlayerCharacter);
	}

	void Die(bool wasKilledByMainPlayerCharacter)
	{
		if (wasKilledByMainPlayerCharacter && humanoidScript != null)
			humanoidScript.DropMoney ();
		
		if (transform.parent != null)
			Destroy (transform.parent.gameObject);

		Destroy (gameObject);
	}

}
