using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public bool canSpawn = true;
    public Transform player;
    public Camera mainCamera;
    public float spawnOffset = 1f; // Offset from camera view for spawning
    private int direction = 1;

    private string sceneName;
    private int count = 0;

    private void Start()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
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
            if (count >= 3 && sceneName == "Mountains")
            {
                Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
                count = 0;
            } else
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                count++;
            }
        }
    }
}



// for multiple types of enemies
//int randomEnemy = Random.Range(0, enemyPrefab.length);
//GameObject enemyToSpawn = enemyPrefab[randomEnemy];