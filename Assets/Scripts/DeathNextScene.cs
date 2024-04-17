using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Task.Delay(5000);
        SceneManager.LoadScene(1);
    }
}
