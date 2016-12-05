using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Threading;

public class WaveController : MonoBehaviour {

    [Serializable]
    public class Wave
    {
        public int lightShips;
        public int mediumShips;
        public int boss1;
        public int boss2;

        public int pirateShips;
    }

    public Wave[] waves;
    public GameObject lightShip;
    public GameObject mediumShip;
    public GameObject boss_1;

    public GameObject pirateShip;
    public GameObject boss_2;

    public float waveDelay = 5.0f;
    public float startFirstWave = 10.0f;
    public float spawnDelay = 0.5f;
    public bool duplicateSpawnPoint = false;

    private float waveCounter = 0;
    public GUIText currentWave;

    private GameController gameController;


    private void Start()
    {
        StartCoroutine(WaveLauncer());
        
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        gameController = gc.GetComponent<GameController>();
    }

    private void Update()
    {
        if (duplicateSpawnPoint != true)
        {
            currentWave.text = "Wave: " + waveCounter + "/" + waves.Length;

            if (waveCounter == waves.Length)
            {               
                StartCoroutine(gameController.WinGame());
            }
        }
    }

    private IEnumerator ShipSpawner(Wave wave)
    {
        for (int i = 0; i < wave.lightShips; i++)
        {
            Instantiate(lightShip, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < wave.pirateShips; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(pirateShip, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < wave.mediumShips; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(mediumShip, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < wave.boss1; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(boss_1, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }

        for (int i = 0; i < wave.boss2; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(boss_2, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }     
    }

    private IEnumerator WaveLauncer()
    {
        yield return new WaitForSeconds(startFirstWave);

        foreach(Wave wave in waves)
        {
            waveCounter++;          
            StartCoroutine(ShipSpawner(wave));
            yield return new WaitForSeconds(waveDelay);
        }
    }

}







