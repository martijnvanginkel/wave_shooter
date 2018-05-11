using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public Text levelText;

	[SerializeField]
	private int currentWave;

	public bool canSpawn;

	public Slug slug;
	public Transform spawnPoint;

	[SerializeField]
	private float spawnTime;

	[SerializeField]
	private float spawnAmount;

	public int currentEnemyCount;

	public bool allSpawned = false;

	// Use this for initialization
	void Start () {

		canSpawn = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		levelText.text = currentWave.ToString();

		if (canSpawn) {

			StartCoroutine("SpawnEnemies", spawnAmount);
		}

		if (currentEnemyCount == 0 && allSpawned) {
			allSpawned = false;
			currentWave += 1;
			canSpawn = true;
		}
	}

	public IEnumerator SpawnEnemies(int enemies) {

		canSpawn = false;

		for(int i = 0; i < enemies; i++)
        {

			yield return new WaitForSeconds(spawnTime);
			Slug newSlug = Instantiate(slug, spawnPoint.position, Quaternion.identity);

			currentEnemyCount += 1;

			// If last enemy of the wave is spawned, allSpawned is true
			if (i == spawnAmount - 1) {
				allSpawned = true;
			}

        }
	}

}
