using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject enemyPrefab;
    public bool canSpawn = true;
    public Transform player;
    public Camera mainCamera;
    public float spawnOffset = 1f; // Offset from camera view for spawning
    private int direction = 1;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        direction = Random.Range(0, 2) * 2 - 1; // Randomly set direction to 1 or -1
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;

            // Calculate the camera boundaries
            float cameraX = mainCamera.transform.position.x;
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            // Calculate spawn position
            float spawnX;
            if (direction == 1)
            {
                spawnX = cameraX + (cameraWidth / 2 + spawnOffset);

            }
            else
            {
                spawnX = cameraX - (cameraWidth / 2 + spawnOffset);
            }
            float playerY = player.position.y;
            // Spawn enemy
            Vector3 spawnPosition = new Vector3(spawnX, playerY, 0f);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}



// for multiple types of enemies
//int randomEnemy = Random.Range(0, enemyPrefab.length);
//GameObject enemyToSpawn = enemyPrefab[randomEnemy];