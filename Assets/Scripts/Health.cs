using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHealth = 50f;
	public float health;
	public GameObject hitEffect;
	public GameObject hitEffectBig;


	// Use this for initialization
	void Start () 
	{
		health = maxHealth;	
	}
		
	public void TakeDamage(float amount)
	{
		health -= amount;

		if (health <= 0)
			Die ();
	}

	void Die()
	{
		Destroy (gameObject);
	}
}
