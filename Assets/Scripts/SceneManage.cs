using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Custom.Scripts;

public class SceneManage : MonoBehaviour
{
    public Animator transition;
    public AudioSource AudioSource;
    public AudioClip playButtonClick;
    public AudioClip optionButtonClick;
    public AudioClip quitButtonClick;
    public int buildIndex = 1;
    public float delayBeforeSceneLoad = 1.0f;

    private void Start()
    {
        transition = GetComponent<Animator>();
        transition.ResetTrigger("Start");
        Time.timeScale = 1;
    }
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void PlayButton()
    {
        AudioSource.PlayOneShot(playButtonClick);
        StartCoroutine(LoadSceneAfterDelay(buildIndex, delayBeforeSceneLoad));
    }

    private IEnumerator LoadSceneAfterDelay(int buildIndex, float delay)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delay);

        // Load the scene after the delay
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        AudioSource.PlayOneShot(playButtonClick);
        StartCoroutine(Wait(delayBeforeSceneLoad));
    }
    private IEnumerator Wait(float delay)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delay);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Click()
    {
        AudioSource.PlayOneShot(playButtonClick);
    }
}
