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
        public int huskRequired = 25;
        public int woodRequired = 25;
        public int stoneRequired = 25;
        private Collider2D objectDetectionCollider;
        void Update()
        {
            if (DetectObject() && InteractInput())
            {
                switch (layerName)
                {
                    case "CastleCore":
                        if (GameManager.Instance.playerResources.GetResourceTotal("Wood") >= woodRequired && GameManager.Instance.playerResources.GetResourceTotal("Stone") >= stoneRequired && GameManager.Instance.playerResources.GetResourceTotal("Husk") >= huskRequired)
                        {
                            GameManager.Instance.IncreaseCoreLevel();
                            Debug.Log("Castle Core upgraded to Level " + GameManager.Instance.GetCoreLevel());
                            GameManager.Instance.playerResources.AddToResourceTotal(-huskRequired, "Husk");
                            GameManager.Instance.playerResources.AddToResourceTotal(-woodRequired, "Wood");
                            GameManager.Instance.playerResources.AddToResourceTotal(-stoneRequired, "Stone");
                            huskRequired += 25;
                            woodRequired += 25;
                            stoneRequired += 25;
                            Debug.Log("You now need " + huskRequired + " Husks, " + woodRequired + " Wood, and " + stoneRequired + " Stone for the next upgrade.");
                        }
                        else
                        {
                            Debug.Log("You need " + huskRequired + " Husks, " + woodRequired + " Wood, and " + stoneRequired + " Stone for the next upgrade!");
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