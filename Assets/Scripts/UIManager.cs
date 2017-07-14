using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject dolarEffect;
	Text moneyCounter;
	float money;


	// Use this for initialization
	void Start () {
		moneyCounter = GameObject.Find ("MoneyCounter").GetComponent<Text> ();
//		dolarEffect = GameObject.Find ("DolarEffect").GetComponent<Text> ();

//		dolarEffect.color.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMoneyCounter(int amount)
	{
		moneyCounter.text = "$" + amount;
	}

	public void DisplayDolarEffect(Vector3 position)
	{
		position.y = 1f;
		Instantiate (dolarEffect, position, dolarEffect.transform.rotation);
	}
}
