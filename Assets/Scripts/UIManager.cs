using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject dolarEffect;
	Text moneyCounter;
	Text waveText;
	Text gameOverText;
	float money;


	// Use this for initialization
	void Start () {
		moneyCounter = GameObject.Find ("MoneyCounter").GetComponent<Text> ();
		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text> ();
		gameOverText.gameObject.SetActive (false);
		GetWaveText ();
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

	public void FadeOutWaveText(int wave)
	{
		if (waveText == null)
			GetWaveText ();

		waveText.text = "Wave " + wave;
		waveText.CrossFadeAlpha (1f, 0f, true);
		waveText.CrossFadeAlpha (0f, 200f * Time.deltaTime, true);
	}

	void GetWaveText()
	{
		waveText = GameObject.Find ("WaveText").GetComponent<Text> ();
	}
		
	public void FadeInGameOverText()
	{
		gameOverText.gameObject.SetActive (true);
		gameOverText.CrossFadeAlpha (0f, 0f, true);
		gameOverText.CrossFadeAlpha (1f, 100f * Time.deltaTime, true);
	}
}
