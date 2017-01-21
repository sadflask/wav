using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject boss;

    public int enemyWait;
    public int waveTime;
    public float enemySpawnHeight;
    public Transform boundary;

    private int numSets;

    // Use this for initialization
    void Start () {
        numSets = 1;
        boundary = player.GetComponent<PlayerController>().boundary;
        StartCoroutine(SpawnWaves());
	}
	
	IEnumerator SpawnWaves()
    {
        
        while(player)
        {
            for(int i=1;i<=10;i++)
            {
                if (i == 10)
                {
                    SendWave(i * numSets, true);
                }
                else
                {
                    Debug.Log("Starting");
                    StartCoroutine(SendWave(i * numSets * 3, false));
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
        yield return null;
    }
}
