using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public Animator transition;

    public void LoadScene(int buildIndex)
    {
        if(buildIndex == 1)
        {
            transition.ResetTrigger("Start");
        }
        SceneManager.LoadScene(buildIndex);

    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
