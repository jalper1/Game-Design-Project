using System.Collections.Generic;
using UnityEngine;
using XEntity.InventoryItemSystem;

namespace Custom.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public ResourceManage playerResources;
        public int coreLevel = 1;
        public int huskRequired = 10;
        public int woodRequired = 25;
        public int stoneRequired = 25;
        public string core = "";
        public string[] dialogueWords;
        public bool levelup = false;

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

        private void Update()
        {
            dialogueWords[0] = core;
            dialogueWords[1] = "";
            if (levelup == true)
            {
                dialogueWords[0] = "Castle Core level upgraded to " + (coreLevel + 1) + "!";
                dialogueWords[1] = core;
            }
        }

    }
}