using Custom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUpgradeManager : MonoBehaviour
{
    private int castleLevel;
    private GameObject destroyedCarpet;
    private GameObject cleanCarpet;

    // 0 indicates the object before upgrade, 1 indicates the object after upgrade
    private GameObject carpet_0;
    private GameObject carpet_1;
    private GameObject throne_0;
    private GameObject throne_1;
    private GameObject webs;
    private GameObject windows_all_0;
    private GameObject windows_all_1;
    private GameObject rocks;
    private GameObject bookshelves;

    void Start()
    {
        carpet_0 = GameObject.Find("carpet_0");
        carpet_1 = GameObject.Find("carpet_1");
        throne_0 = GameObject.Find("throne_0");
        throne_1 = GameObject.Find("throne_1");
        windows_all_0 = GameObject.Find("windows_all_0");
        windows_all_1 = GameObject.Find("windows_all_1");
        rocks = GameObject.Find("rocks");
        webs = GameObject.Find("webs");
        bookshelves = GameObject.Find("bookshelves");
        castleLevel = 1;

        // Set the base castle visuals
        setBaseCastle();
    }

    void Update()
    {
        castleLevel = GameManager.Instance.coreLevel;
        switch (castleLevel)
        {
            // case 1 is the base level
            case 1:
                setBaseCastle();
                break;
            case 2:
                webs.SetActive(false);
                rocks.SetActive(false);
                break;
            case 3:
                upgradeWindows();
                break;
            case 4:
                upgradeCarpet();
                break;
            case 5:
                upgradeThrone();
                break;
        }
    }

    void setBaseCastle()
    {
        throne_0.SetActive(true);
        carpet_0.SetActive(true);
        webs.SetActive(true);
        windows_all_0.SetActive(true);
        rocks.SetActive(true);
        bookshelves.SetActive(true);

        carpet_1.SetActive(false);
        throne_1.SetActive(false);
        windows_all_1.SetActive(false);
    }
    void upgradeCarpet()
    {
        upgradeWindows();
        carpet_0.SetActive(false);
        carpet_1.SetActive(true);
    }

    void upgradeThrone()
    {
        upgradeCarpet();
        throne_0.SetActive(false);
        throne_1.SetActive(true);
    }

    void upgradeWindows()
    {
        webs.SetActive(false);
        rocks.SetActive(false);
        windows_all_0.SetActive(false);
        windows_all_1.SetActive(true);
    }

}
