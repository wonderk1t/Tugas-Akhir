using System.Collections;
using UnityEngine;

public class WaveSpawnerMultiLane : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;
    public Transform[] pathParents;

    public int enemyPerWave = 10;
    public float spawnDelay = 1f;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyPerWave; i++)
        {
            int lane = Random.Range(0, spawnPoints.Length);

            GameObject enemy =
                Instantiate(enemyPrefab,
                spawnPoints[lane].position,
                Quaternion.identity);

            EnemyFSM fsm = enemy.GetComponent<EnemyFSM>();

            Transform path = pathParents[lane];

            fsm.waypoints = new Transform[path.childCount];

            for (int j = 0; j < path.childCount; j++)
            {
                fsm.waypoints[j] = path.GetChild(j);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}