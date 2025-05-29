using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 2f;
    private float elapsedTime = 0f;
    private float interval = 10f;
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            elapsedTime += timeBetweenSpawns;
            if (elapsedTime >= interval)
            {
                timeBetweenSpawns = Mathf.Max(timeBetweenSpawns - 1, 1);
                elapsedTime = 0f;
            } 
                
        }
    }
    
}
