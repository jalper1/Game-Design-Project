using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Scripts
{
    public class ResourceManage : MonoBehaviour
    {
        int woodAmount = 0;
        int stoneAmount = 0;
        int huskAmount = 0;

        // use scriptable object to store resources

        void Start()
        {
            woodAmount = 0;
            stoneAmount = 0;
            huskAmount = 0;
            GameManager.Instance.playerResources = this;
        }
        public void AddToResourceTotal(int amount, ResourceType type)
        {
            if (type == ResourceType.Wood)
            {
                woodAmount += amount;
            }
            else if (type == ResourceType.Stone)
            {
                stoneAmount += amount;
            }
            else if (type == ResourceType.Husk)
            {
                huskAmount += amount;
            }
            Debug.Log("You have " + stoneAmount + " of stone");
            Debug.Log("You have " + woodAmount + " of wood");
            Debug.Log("You have " + huskAmount + " of husk");
        }
        public int GetResourceTotal(ResourceType type)
        {
            if (type == ResourceType.Wood)
            {
                return woodAmount;
            }
            else if (type == ResourceType.Stone)
            {
                return stoneAmount;
            }
            else if (type == ResourceType.Husk)
            {
                return huskAmount;
            }
            return 0;
        }
    }
}