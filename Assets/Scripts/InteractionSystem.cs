using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                            GameManager.Instance.IncreaseCoreLevel();
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.huskRequired, "Husk");
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.woodRequired, "Wood");
                            GameManager.Instance.playerResources.AddToResourceTotal(-GameManager.Instance.stoneRequired, "Stone");
                            GameManager.Instance.huskRequired += 10;
                            GameManager.Instance.woodRequired += 25;
                            GameManager.Instance.stoneRequired += 25;
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