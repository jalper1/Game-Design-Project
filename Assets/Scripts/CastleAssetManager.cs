using UnityEngine;
using System.Collections.Generic;

public class CastleManager : MonoBehaviour
{
    public int coreLevel;

    // Serialize the assetLevels dictionary to make it visible in the Unity Inspector
    [SerializeField]
    private Dictionary<GameObject, int> assetLevels = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize your assetLevels dictionary here by adding asset GameObjects and their required castle levels
        // Example:
        // assetLevels.Add(assetGameObject, requiredCastleLevel);

        // You can also populate assetLevels through the Unity Inspector
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate through assetLevels dictionary and switch assets if required castle level is met
        foreach (KeyValuePair<GameObject, int> kvp in assetLevels)
        {
            if (coreLevel >= kvp.Value && kvp.Key.activeSelf) // Check if core level requirement is met and asset is active
            {
                SwitchAssetToFixedVersion(kvp.Key);
            }
        }
    }

    void SwitchAssetToFixedVersion(GameObject asset)
    {
        // Activate fixed version and deactivate broken version for the specified asset
        asset.SetActive(false); // Deactivate broken version

        // Assuming the fixed version is a child GameObject named "FixedVersion"
        foreach (Transform child in asset.transform)
        {
            if (child.name == "FixedVersion")
            {
                child.gameObject.SetActive(true); // Activate fixed version
                break;
            }
        }
    }
}
