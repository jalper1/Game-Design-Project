using Custom.Scripts;
using JetBrains.Annotations;
using System.Collections;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vitals;
using XEntity.InventoryItemSystem;

public class RespawnManager : MonoBehaviour
{
    private static RespawnManager _instance;
    private GameObject player;
    private GameObject spawnPoint;
    public GameObject mountainSpawnPoint;
    private bool respawn = false;
    public int playerLife = 100;
    public bool transition = false;
    public VitalsUIBind bindComponent;
    public PlayerCharacter playerCharacter;
    public bool mountainTransition = false;

    public AudioSource AudioSource;
    public AudioClip deathSound;
    public AudioClip spawnSound;

    public static RespawnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Respawn Manager is null");

            return _instance;
        }

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // This function is invoked when the scene is loaded.
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Find player and spawn point in the initial scene
        player = GameObject.Find("Player");
        spawnPoint = GameObject.Find("SpawnPoint");
        mountainSpawnPoint = GameObject.Find("MountainSpawnPoint");

        // Set player's position to spawn point's position in the initial scene
        if (player != null && spawnPoint != null && !transition)
        {
            player.transform.position = spawnPoint.transform.position;
            playerCharacter = player.GetComponent<PlayerCharacter>();
            bindComponent = playerCharacter.healthBar.GetComponent<VitalsUIBind>();
        }
        else
        {
            Debug.LogError("Player or SpawnPoint not found in initial scene.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when this object is destroyed.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        if(scene.buildIndex == 1 && mountainTransition && !respawn)
        {
            mountainSpawnPoint = GameObject.Find("MountainSpawnPoint");
            if (player != null && mountainSpawnPoint != null)
            {
                player.transform.position = mountainSpawnPoint.transform.position;
            }
            else
            {
                Debug.LogError("Player or MountainSpawnPoint not found in scene.");
            }
        }
        // This function is called when a new scene is loaded.
        if (scene.buildIndex == 1 && !transition) // Assuming SpawnPoint is only relevant in scene index 1
        {
            spawnPoint = GameObject.Find("SpawnPoint");

            if (player != null && spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
                respawn = false;
                switch (GameManager.Instance.coreLevel)
                {
                    case 1:
                        playerLife = 100;
                        break;
                    case 2:
                        playerLife = 120;
                        break;
                    case 3:
                        playerLife = 140;
                        break;
                    case 4:
                        playerLife = 160;
                        break;
                    default:
                        playerLife = 100;
                        break;
                }
            }
            else
            {
                Debug.LogError("Player or SpawnPoint not found in scene.");
            }
        }
        else
        {
            transition = false;
        }
    }

    private void Update()
    {
        player = GameObject.Find("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
        bindComponent = playerCharacter.healthBar.GetComponent<VitalsUIBind>();

        if (playerLife <= 0)
        {
            respawn = true;
        }

        if (respawn)
        {
            AudioSource.PlayOneShot(deathSound);
            ItemManager.Instance.itemList.Clear();
            SceneManager.LoadScene(1);

        }
        if (player != null && playerCharacter != null)
        {

            playerCharacter.health.Set(playerLife);
            bindComponent.UpdateImage(playerCharacter.health.Value, playerCharacter.health.MaxValue, false);
        }
        //Debug.Log("health:" + playerLife);
        //Debug.Log("health:" + playerCharacter.health.Value);
    }
}
