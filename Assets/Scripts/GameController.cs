using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;
    public GameObject dodger;
    public GameObject bruiser;
    public GameObject bossEnemy;
	public GameObject asteroid;
	public GameObject zoomer;

    public int enemyWait;
    public int waveTime;
    public float enemySpawnHeight;
    public Transform boundary;
    public Text waveText;
	public GameObject explosion;

	public float asteroidSpawnDelay;

    private bool bossDestroyed;
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
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene("title");

	}
	public void PlayExplosion() {
		StartCoroutine (Explosions ());
	}
	IEnumerator Explosions() {
		for (int i = 0; i < 15; i++) {
			Vector3 randomPosition = new Vector3 (Random.Range (-5, 5), Random.Range (-3, 3));
			Instantiate (explosion, randomPosition, transform.rotation);
			yield return new WaitForSeconds (0.5f);
		}
		yield return null;
	}
	IEnumerator SpawnWaves()
    {
        
        while(player)
        {
            for(int i=1;i<=10;i++)
            {
                waveText.text = "Wave: " + (i + 10 * numSets).ToString();
                if (i == 5)
                {
                    bossDestroyed = false;
                    StartCoroutine(SendWave(i + 10 * numSets, true));
                    yield return new WaitUntil(() => bossDestroyed);
                }
                else
                {
                    Debug.Log("Starting");
                    StartCoroutine(SendWave(i + 10 * numSets, false));
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
        for (int i = 0; i < numEnemies / 2; i++)
        {
            //Spawn enemy at random position
            Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x / 2, boundary.localScale.x / 2), enemySpawnHeight);
            Instantiate(zoomer, randomPosition, Quaternion.Euler(new Vector3(90, 180, 0)));
            //Delay
            yield return new WaitForSeconds(enemyWait);
        }
    for (int i=0;i<numEnemies/5;i++)
        {
            //Spawn next level enemy at random position
            Vector3 randomPosition = new Vector3(Random.Range(-boundary.localScale.x / 2, boundary.localScale.x / 2), enemySpawnHeight);
            Instantiate(bruiser, randomPosition, Quaternion.Euler(new Vector3(-90, 180, 0)));
            //Delay
            yield return new WaitForSeconds(enemyWait);
        }
        yield return new WaitForSeconds(5);

        GameObject bossMan = null;
        //
        if (boss)
        {
            //Send wave of enemies and instantiate boss
            for (int i=0; i< numSets * 2; i++)
            {
                //Instantiate bruiser
                Vector3 position = new Vector3(Random.Range(-boundary.localScale.x / 2, boundary.localScale.x / 2), enemySpawnHeight);
                Instantiate(bruiser, position, Quaternion.Euler(new Vector3(-90, 180, 0)));
            }
            //Wait
            yield return new WaitForSeconds(3);
            //Instantiate boss
            Vector3 randomPosition = new Vector3(0, enemySpawnHeight);
            bossMan = Instantiate(bossEnemy, randomPosition, Quaternion.Euler(90, 180, 0));
        }

        while (bossMan)
        {
            yield return new WaitForSeconds(0.1f);
        }
        bossDestroyed = true;
        yield return null;
    }
}
