using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;
    public Transform[] pathParents;

    public int waveNumber = 1;
    public int enemyPerWave = 5;

    public float timeBetweenSpawn = 3f;
    public float timeBetweenWave = 5f;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            Debug.Log("Wave " + waveNumber);

            // Spawn enemy untuk wave ini
            for (int i = 0; i < enemyPerWave; i++)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(timeBetweenSpawn);
            }

            // Tunggu semua enemy mati
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null;
            }

            Debug.Log("Wave Selesai");

            yield return new WaitForSeconds(timeBetweenWave);

            // Naik wave
            waveNumber++;

            // Tambah jumlah enemy
            enemyPerWave += 2;
        }
    }

    void SpawnEnemy()
    {
        int laneIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoints[laneIndex].position,
            Quaternion.identity
        );

        EnemyFSM fsm = enemy.GetComponent<EnemyFSM>();

        if (fsm != null)
        {
            Transform path = pathParents[laneIndex];

            fsm.waypoints = new Transform[path.childCount];

            for (int i = 0; i < path.childCount; i++)
            {
                fsm.waypoints[i] = path.GetChild(i);
            }
        }
    }
}