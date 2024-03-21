using UnityEngine;
using UnityEngine.SceneManagement;
using XEntity.InventoryItemSystem;

namespace Custom.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public ResourceManage playerResources;
        public ItemManager itemManager;
        public int coreLevel = 1;

        // doing something don't interrupt
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Game Manager is null");

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

        public void IncreaseCoreLevel()
        {
            coreLevel++;
        }
        public int GetCoreLevel()
        {
            return coreLevel;
        }
    }
}