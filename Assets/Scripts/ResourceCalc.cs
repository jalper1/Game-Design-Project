using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEntity.InventoryItemSystem;

public enum ResourceType
{
    Wood,
    Stone,
    Husk
}

public struct Resource
{
    public ResourceType type;
    public int resourcesGained;
}

public class ResourceCalc : MonoBehaviour
{
    Interactor interactor;
    public Item harvestItem;

    public int resourceAmount = 10;
    public ResourceType resourceType;
    private Resource resource;
    public int maxResource = 50;
    public int minResource = 25;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<Interactor>();
        resourceAmount = Random.Range(minResource, maxResource);
    }
    public void CollectResource(int amount)
    {
        resource.type = resourceType;
        resourceAmount -= amount;

        if (resourceAmount <= 0)
        {
            StartCoroutine(Utils.TweenScaleOut(gameObject, 40, true));
            resource.resourcesGained = amount + resourceAmount;
        }
        else
        {
            resource.resourcesGained = amount;
        }
        interactor.AddToInventory(harvestItem, resource.resourcesGained);
    }
}
