using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEntity.InventoryItemSystem;

public struct Resource
{
    public string type;
    public int resourcesGained;
}

public class ResourceCalc : MonoBehaviour
{
    Interactor interactor;
    public Item harvestItem;
    private List<Item> itemList;

    public int resourceAmount = 10;
    public string resourceType;
    private Resource resource;
    public int maxResource = 50;
    public int minResource = 25;

    // Start is called before the first frame update
    void Start()
    {
        itemList = Custom.Scripts.GameManager.Instance.itemManager.itemList;
        interactor = GetComponent<Interactor>();
        resourceAmount = Random.Range(minResource, maxResource);
    }
    public (Item, int) CollectResource(int amount)
    {
        resource.type = resourceType;
        resourceAmount -= amount;

        if (resourceAmount <= 0)
        {
            StartCoroutine(Utils.TweenScaleOut(gameObject, 40, true));
            if (harvestItem.name == "Wood") { StartCoroutine(Utils.TweenScaleOut(gameObject.transform.parent.gameObject, 40, true)); }
            resource.resourcesGained = amount + resourceAmount;
        }
        else
        {
            resource.resourcesGained = amount;
        }
        interactor.AddToInventory(harvestItem, resource.resourcesGained);
        itemList.Add(harvestItem);
        return (harvestItem, resource.resourcesGained);
    }
}
