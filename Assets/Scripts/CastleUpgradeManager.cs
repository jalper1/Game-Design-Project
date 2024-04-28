using Custom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUpgradeManager : MonoBehaviour
{
    private int castleLevel;
    private GameObject destroyedCarpet;
    private GameObject cleanCarpet;
    // Start is called before the first frame update
    void Update()
    {
        castleLevel = GameManager.Instance.coreLevel;
        destroyedCarpet = GameObject.Find("Carpet Destroyed");
        cleanCarpet = GameObject.Find("CarpetClean");
        cleanCarpet.SetActive(false);


        switch (castleLevel)
        {
            case 1:
                break;
            case 2:
                cleanCarpet.SetActive(true);
                destroyedCarpet.SetActive(false);
                break;
            case 3:
                // do something
                break;
            case 4:
                // do something
                break;
            case 5:
                // winner
                break;
        }
        
    }

}
