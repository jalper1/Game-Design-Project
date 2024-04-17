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
        public void AddToResourceTotal(int amount, string type)
        {
            if (type == "Wood")
            {
                woodAmount += amount;                
            }
            else if (type == "Stone")
            {
                stoneAmount += amount;
            }
            else if (type == "Husk")
            {
                huskAmount += amount;
            }
        }
        public int GetResourceTotal(string type)
        {
            if (type == "Wood")
            {
                return woodAmount;
            }
            else if (type == "Stone")
            {
                return stoneAmount;
            }
            else if (type == "Husk")
            {
                return huskAmount;
            }
            return 0;
        }
    }
}