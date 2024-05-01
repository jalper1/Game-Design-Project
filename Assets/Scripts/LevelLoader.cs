using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vitals;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private bool playerEnteredTrigger = false;
    public int levelToLoad;

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
            if(levelToLoad == 1)
            {
                RespawnManager.Instance.transition = true;
            }
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                RespawnManager.Instance.mountainTransition = true;
            }
            StartCoroutine(LoadLevel(levelToLoad));
        }
    }

    // Coroutine to load the level with transition
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
