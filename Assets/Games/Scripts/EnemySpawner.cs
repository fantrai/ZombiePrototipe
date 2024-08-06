using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.PlayerSettings;

public class EnemySpawner : MonoBehaviour
{
    [Inject] DiContainer container;

    [SerializeField] Enemy enemyPrefab;
    [SerializeField, Min(0)] int startCountEnemyInWave;
    [SerializeField, Min(0)] float timeBeforNewWave;
    [SerializeField, Min(0)] int addEnemyInNewWave;
    [SerializeField] GameObject[] spawnPos;

    float intervalSpawn = 0.5f;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        int countEnemyInWave = startCountEnemyInWave;

        do
        {
            for (int i = 0; i < countEnemyInWave; i++)
            {
                Create();
                yield return new WaitForSeconds(intervalSpawn);
            }

            yield return new WaitForSeconds(timeBeforNewWave);

            countEnemyInWave += addEnemyInNewWave;
        } while (true);
    }

    public void Create()
    {
        if (spawnPos != null)
        {
            var pos = spawnPos[Random.Range(0, spawnPos.Length)].transform.position;
            container.InstantiatePrefab(enemyPrefab, pos, enemyPrefab.transform.rotation, transform);
        }
        else
        {
            container.InstantiatePrefab(enemyPrefab, transform.position, enemyPrefab.transform.rotation, transform);
        }
    }
}
