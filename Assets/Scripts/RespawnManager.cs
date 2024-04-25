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
    private bool respawn = false;
    public int playerLife = 100;
    public bool transition = false;

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

        // Set player's position to spawn point's position in the initial scene
        if (player != null && spawnPoint != null && !transition)
        {
            player.transform.position = spawnPoint.transform.position;
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
        // This function is called when a new scene is loaded.
        if (scene.buildIndex == 1 && !transition) // Assuming SpawnPoint is only relevant in scene index 1
        {
            spawnPoint = GameObject.Find("SpawnPoint");

            if (player != null && spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
                respawn = false;
                playerLife = 100;
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
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        if (GameManager.Instance.levelup)
        {
            switch (GameManager.Instance.coreLevel)
            {
                case 1:
                    GameManager.Instance.harvestStrength = 1;
                    playerLife = 100;
                    break;
                case 2:
                    GameManager.Instance.harvestStrength = 2;
                    playerLife = 120;
                    break;
                case 3:
                    GameManager.Instance.harvestStrength = 3;
                    playerLife = 140;
                    break;
                case 4:
                    GameManager.Instance.harvestStrength = 4;
                    playerLife = 160;
                    break;
                default:
                    GameManager.Instance.harvestStrength = 1;
                    playerLife = 100;
                    break;
            }
            playerCharacter.health.Set(playerLife);
            playerCharacter.health.SetMax(playerLife);
        }
        if (playerLife <= 0)
        {
            respawn = true;
        }

        if (respawn)
        {
            SceneManager.LoadScene(1);
            ItemManager.Instance.itemList.Clear();
        }
        if (player != null)
        {
            if (playerCharacter != null)
            {
                playerCharacter.health.Set(playerLife);
                VitalsUIBind bindComponent = playerCharacter.healthBar.GetComponent<VitalsUIBind>();
                bindComponent.UpdateImage(playerCharacter.health.Value, playerCharacter.health.MaxValue, false);
            }
        }
    }
}
