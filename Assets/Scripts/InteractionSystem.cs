using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XEntity.InventoryItemSystem;

namespace Custom.Scripts
{
    public class InteractionSystem : MonoBehaviour
    {

        // Update is called once per frame
        public Transform playerInteractPoint;
        public float detectionRadius = 0.2f;
        public LayerMask detectionLayer;
        private string layerName = null;
        private Collider2D objectDetectionCollider;

        public Item wood;
        public Item stone;
        public Item husk;

        ItemContainer itemContainer;

        private void Start()
        {
            GameManager.Instance.core = "You need " + GameManager.Instance.huskRequired + " Husks, " + GameManager.Instance.woodRequired + " Wood, and " + GameManager.Instance.stoneRequired + " Stone for the next upgrade!";
        }
        void Update()
        {
            if (GameManager.Instance.playerResources.GetResourceTotal("Wood") >= GameManager.Instance.woodRequired &&
                            GameManager.Instance.playerResources.GetResourceTotal("Stone") >= GameManager.Instance.stoneRequired &&
                            GameManager.Instance.playerResources.GetResourceTotal("Husk") >= GameManager.Instance.huskRequired)
            {
                GameManager.Instance.levelup = true;
            }

            if (DetectObject() && InteractInput())
            {
                switch (layerName)
                {
                    case "CastleCore":
                        if (GameManager.Instance.playerResources.GetResourceTotal("Wood") >= GameManager.Instance.woodRequired &&
                            GameManager.Instance.playerResources.GetResourceTotal("Stone") >= GameManager.Instance.stoneRequired &&
                            GameManager.Instance.playerResources.GetResourceTotal("Husk") >= GameManager.Instance.huskRequired)
                        {
                            if (GameManager.Instance.win)
                            {
                                GameManager.Instance.core = "Your castle core is fully upgraded!";
                                return;
                            }
                            GameManager.Instance.IncreaseCoreLevel();
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.huskRequired, "Husk");
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.woodRequired, "Wood");
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.stoneRequired, "Stone");
                            int woodCount = 0;
                            int stoneCount = 0;
                            int huskCount = 0;
                            for (int j = 0; j < ItemManager.Instance.itemList.Count; j++)
                            {
                                if (ItemManager.Instance.itemList[j].name == "Wood")
                                {
                                    woodCount++;
                                }
                                else if (ItemManager.Instance.itemList[j].name == "Stone")
                                {
                                    stoneCount++;
                                }
                                else if (ItemManager.Instance.itemList[j].name == "Husk")
                                {
                                    huskCount++;
                                }
                            }

                            woodCount -= GameManager.Instance.woodRequired;
                            stoneCount -= GameManager.Instance.stoneRequired;
                            huskCount -= GameManager.Instance.huskRequired;
                            ItemManager.Instance.itemList.Clear();
                            GameObject playerInventory = GameObject.Find("Player Inventory");
                            itemContainer = playerInventory.GetComponent<ItemContainer>();
                            itemContainer.clearInv();
                            for (int i = 0; i < woodCount; i++)
                            {
                                ItemManager.Instance.itemList.Add(wood);
                                itemContainer.AddItem(wood);
                            }

                            for (int i = 0; i < stoneCount; i++)
                            {
                                ItemManager.Instance.itemList.Add(stone);
                                itemContainer.AddItem(stone);
                            }

                            for (int i = 0; i < huskCount; i++)
                            {
                                ItemManager.Instance.itemList.Add(husk);
                                itemContainer.AddItem(husk);
                            }
                            GameManager.Instance.huskRequired += 5;
                            GameManager.Instance.woodRequired += 10;
                            GameManager.Instance.stoneRequired += 10;
                            GameManager.Instance.core = "You now need " + GameManager.Instance.huskRequired + " Husks, " + GameManager.Instance.woodRequired + " Wood, and " + GameManager.Instance.stoneRequired + " Stone for the next upgrade.";
                        }
                        else
                        {
                            GameManager.Instance.core = "You need " + GameManager.Instance.huskRequired + " Husks, " + GameManager.Instance.woodRequired + " Wood, and " + GameManager.Instance.stoneRequired + " Stone for the next upgrade!";
                        }
                        break;
                    case "Item":
                        Debug.Log("You pick up the item!");
                        objectDetectionCollider.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }


        bool InteractInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        bool DetectObject()
        {
            objectDetectionCollider = Physics2D.OverlapCircle(playerInteractPoint.position, detectionRadius, detectionLayer);
            if (objectDetectionCollider != null)
            {
                layerName = LayerMask.LayerToName(objectDetectionCollider.gameObject.layer);
                return true;
            }
            return false;
        }


        void OnDrawGizmosSelected()
        {
            if (transform == null)
                return;
            Gizmos.DrawWireSphere(playerInteractPoint.position, detectionRadius);
        }

    }
}