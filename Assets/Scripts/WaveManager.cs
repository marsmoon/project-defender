using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {

	public float numberEnemiesInStartingWave = 10;
	public float multiplier = 1;
	public float winConditionCheckInterval = 5f;
	public float winConditionTimer;
	int currentWave;
	Spawner[] spawners;
	UIManager uiManager;
	GameObject playerCharacter;
	bool isGameOver;

	public enum EnemyType
	{
		Zombie
	};
		
	public List<SpawnableEnemy> spawnableEnemies;

	// Use this for initialization
	void Start () {
		isGameOver = false;
		currentWave = 1;
		spawners = GameObject.FindObjectsOfType<Spawner> ();
		uiManager = GetComponent<UIManager> ();
		winConditionTimer = winConditionCheckInterval;
		StartWave ();
		playerCharacter = GameObject.Find ("PlayerTurret");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isGameOver)
			return;
		
		winConditionTimer -= Time.deltaTime;

		if (winConditionTimer <= 0)
		{
			if (playerCharacter == null)
			{
				GameOver ();
				return;
			}

			if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0)
			{
				currentWave++;
				StartWave ();
			}

			winConditionTimer = winConditionCheckInterval;
		}
	}

	void StartWave()
	{
		int numberEnemies = Mathf.RoundToInt(numberEnemiesInStartingWave * (multiplier * currentWave));
		int curNumberEnemies = numberEnemies;

		while (curNumberEnemies > 0)
		{
			foreach (Spawner spawner in spawners)
			{
				if (curNumberEnemies <= 0)
					break;
				
				if (Random.Range (0, 2) == 1)
				{
//					if (spawnableEnemies [0].enemyType == EnemyType.Zombie)
					spawner.Spawn (spawnableEnemies [0].prefab);
					curNumberEnemies--;
				}
			}
		}

		uiManager.FadeOutWaveText (currentWave);
	}

	void GameOver()
	{
		isGameOver = true;
		Time.timeScale = 0f;
		uiManager.FadeInGameOverText ();
	}

	public void RestartLevel()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
