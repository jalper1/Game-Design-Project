using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int resourceAmount = 10;
    public ResourceType resourceType;
    private Resource resource;
    public int maxResource = 50;
    public int minResource = 25;

    // Start is called before the first frame update
    void Start()
    {
        resourceAmount = Random.Range(minResource, maxResource);
    }
    public Resource CollectResource(int amount)
    {
        resource.type = resourceType;
        resourceAmount -= amount;

        if (resourceAmount <= 0)
        {
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            resource.resourcesGained = amount + resourceAmount;
        }
        else
        {
            resource.resourcesGained = amount;
        }
        return resource;
    }
}
