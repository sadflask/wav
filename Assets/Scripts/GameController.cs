using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject enemyL2;
    public GameObject bossEnemy;
	public GameObject asteroid;


    public int enemyWait;
    public int waveTime;
    public float enemySpawnHeight;
    public Transform boundary;
    public Text waveText;

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
                if (i == 10)
                {
                    StartCoroutine(SendWave(i + 10 * numSets, true));
                }
                else
                {
                    Debug.Log("Starting");
                    StartCoroutine(SendWave(i + 10 * numSets, false));
                }
                //Wait to send the next wave
                yield return new WaitForSeconds(waveTime);
            }
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
            Instantiate(enemy, randomPosition, Quaternion.identity);
            //Delay
            yield return new WaitForSeconds(enemyWait);
        }
        for(int i=0;i<numEnemies/5;i++)
        {
            //Spawn next level enemy at random position
            Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x / 2, boundary.localScale.x / 2), enemySpawnHeight);
            Instantiate(enemyL2, randomPosition, Quaternion.identity);
            //Delay
            yield return new WaitForSeconds(enemyWait);
        }
        yield return new WaitForSeconds(5);
        if (boss)
        {
            Vector3 randomPosition = new Vector3(0, enemySpawnHeight);
            Instantiate(bossEnemy, randomPosition, Quaternion.identity);
        }
        yield return null;
    }
}
