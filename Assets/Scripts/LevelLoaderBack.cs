using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vitals;

public class LevelLoaderBack : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private bool playerEnteredTrigger = false;

    private GameObject player;

    // Detect when the player enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredTrigger = true;
        }
    }

    private void Update()
    {
        // Check if the player entered the trigger, then initiate level transition
        if (playerEnteredTrigger)
        {
            playerEnteredTrigger = false; // Reset flag
            LoadNextLevel();
        }
    }

    // Load the next level
    public void LoadNextLevel()
    {
        RespawnManager.Instance.transition = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentSceneIndex - 1));
    }

    // Coroutine to load the level with transition
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        player = GameObject.Find("Player");
        player.transform.localScale = new Vector3(-1, 1, 1);

        if (player != null)
        {
            PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
            if (playerCharacter != null)
            {
                playerCharacter.health.Set(RespawnManager.Instance.playerLife);
                Debug.Log("Player life: " + RespawnManager.Instance.playerLife);
                Debug.Log("Player Health: " + playerCharacter.health.Value);
            }
        }
    }
}
