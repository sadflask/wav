using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject player;
	public GameObject dodger;
	public GameObject bruiser;
	public GameObject bossEnemy;
	public GameObject asteroid;
	public GameObject trashMob;

	public int enemyWait;
	public int waveTime;
	public float enemySpawnHeight;
	public float bossSpawnHeight;
	public Transform boundary;
	public Text waveText;
	public float bossAddSpawnDelay;
	public float asteroidSpawnDelay;


	private int numSets;


	// Use this for initialization
	void Start () {
		numSets = 0;
		boundary = player.GetComponent<PlayerController>().boundary;
		StartCoroutine(SpawnWaves());
		StartCoroutine (Asteroids ());
	}

	// Update is called once per frame
	IEnumerator BossSpawnAdds () {
		while (bossEnemy) {
			Debug.Log ("Making trash mobs");

			GameObject localMob01 = Instantiate(trashMob, new Vector3(-5, 2, 0), Quaternion.Euler(new Vector3(90, 180, 0)));
			GameObject localMob02 = Instantiate(trashMob, new Vector3(0, 2, 0), Quaternion.Euler(new Vector3(90, 180, 0)));
			GameObject localMob03 = Instantiate (trashMob, new Vector3 (5, 2, 0), Quaternion.Euler (new Vector3 (90, 180, 0)));

			yield return new WaitForSeconds(bossAddSpawnDelay);

		}

	}
	// Update is called once per frame
	IEnumerator Asteroids () {
		while (player) {
			Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x/2, boundary.localScale.x / 2), enemySpawnHeight);

			GameObject localAsteroid = Instantiate(asteroid, randomPosition, Quaternion.identity);
			Rigidbody rb = localAsteroid.GetComponent<Rigidbody> ();
			Vector3 v = rb.velocity;
			v.x = Random.Range(-10, 10);
			rb.velocity = v;

			yield return new WaitForSeconds(asteroidSpawnDelay);

		}

	}
	IEnumerator SpawnWaves()
	{

		while(player)
		{
			for(int i=1;i<=10;i++)
			{
				waveText.text = "Wave: " + (i + 10 * numSets).ToString();

				//boss testing - remove this
				if (i == 10)
				{
					StartCoroutine(SendWave(i + 10 * numSets, true));
				}
				else
				{
					Debug.Log("Starting");
					StartCoroutine(SendWave(i + 10 * numSets, true));
				}
				//Wait to send the next wave
				yield return new WaitForSeconds(waveTime);
			}
			numSets++;
		}
		//Spawn waves up until the number of 
		yield return null;
	}
	IEnumerator SendWave(int numEnemies, bool boss)
	{
		//Send one wave of enemies
		Debug.Log(string.Format("Sending wave: {0}", numEnemies));
		for(int i=0;i<numEnemies; i++)
		{
			//Spawn enemy at random position
			Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x/2, boundary.localScale.x / 2), enemySpawnHeight);
			Instantiate(dodger, randomPosition, Quaternion.Euler(new Vector3(90, 180, 0)));
			//Delay
			yield return new WaitForSeconds(enemyWait);
		}
		for(int i=0;i<numEnemies/5;i++)
		{
			//Spawn next level enemy at random position
			Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x / 2, boundary.localScale.x / 2), enemySpawnHeight);
			Instantiate(bruiser, randomPosition, Quaternion.Euler(new Vector3(-90, 180, 0)));
			//Delay
			yield return new WaitForSeconds(enemyWait);
		}
		yield return new WaitForSeconds(5);
		if (boss)
		{
			Vector3 randomPosition = new Vector3(0, bossSpawnHeight);
			GameObject localBoss = Instantiate(bossEnemy, randomPosition, Quaternion.identity);

			StartCoroutine (BossSpawnAdds ());
		}
		yield return null;
	}
}
