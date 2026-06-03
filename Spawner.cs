using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Point per Lane")]
    public Transform[] spawnPoints;

    [Header("Path per Lane")]
    public Transform[] pathParents;

    [Header("Wave Settings")]
    public int enemyPerWave = 5;
    public float timeBetweenSpawn = 1f;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || pathParents.Length == 0)
        {
            Debug.LogError("Spawn Point atau Path belum diisi!");
            return;
        }

        int laneIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoints[laneIndex].position,
            Quaternion.identity
        );

        EnemyFSM fsm = enemy.GetComponent<EnemyFSM>();

        if (fsm != null)
        {
            Transform selectedPath = pathParents[laneIndex];

            Transform[] waypoints = new Transform[selectedPath.childCount];

            for (int i = 0; i < selectedPath.childCount; i++)
            {
                waypoints[i] = selectedPath.GetChild(i);
            }

            fsm.waypoints = waypoints;
        }
    }
}