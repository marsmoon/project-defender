using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public float numberEnemiesInStartingWave = 10;
	public float multiplier = 1;
	public float winConditionCheckInterval = 5f;
	public float winConditionTimer;
	int currentWave;
	Spawner[] spawners;

	public enum EnemyType
	{
		Zombie
	};
		
	public List<SpawnableEnemy> spawnableEnemies;

	// Use this for initialization
	void Start () {
		currentWave = 1;
		spawners = GameObject.FindObjectsOfType<Spawner> ();
		winConditionTimer = winConditionCheckInterval;
		StartWave ();
	}
	
	// Update is called once per frame
	void Update () {
		winConditionTimer -= Time.deltaTime;

		if (winConditionTimer <= 0)
		{
			if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0)
			{
				currentWave++;
				StartWave ();
				winConditionTimer = winConditionCheckInterval;
			}
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
	}

}
