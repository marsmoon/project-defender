using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	Text moneyCounter;
	float money;


	// Use this for initialization
	void Start () {
		moneyCounter = GameObject.Find ("MoneyCounter").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMoneyCounter(int amount)
	{
		moneyCounter.text = "$" + amount;
	}
}
