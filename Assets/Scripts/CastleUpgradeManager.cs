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
    private GameObject bookshelves_0;
    private GameObject bookshelves_1;

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
        bookshelves_0 = GameObject.Find("bookshelves_0");
        bookshelves_1 = GameObject.Find("bookshelves_1");
        castleLevel = GameManager.Instance.coreLevel;

        if (GameManager.Instance.coreLevel == 1)
        {
            setBaseCastle();
        }
    }

    void Update()
    {
        if (GameManager.Instance.coreLevel != 1)
        {
            castleLevel = GameManager.Instance.coreLevel;
            setBaseCastle();

            switch (castleLevel)
            {
                case 6:
                    upgradeThrone();
                    goto case 5;
                case 5:
                    webs.SetActive(false);
                    goto case 4;
                case 4:
                    upgradeCarpet();
                    upgradeWindows();
                    goto case 3;
                case 3:
                    upgradeBookshelves();
                    goto case 2;
                case 2:
                    rocks.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    void setBaseCastle()
    {
        throne_0.SetActive(true);
        carpet_0.SetActive(true);
        webs.SetActive(true);
        windows_all_0.SetActive(true);
        rocks.SetActive(true);
        bookshelves_0.SetActive(true);

        carpet_1.SetActive(false);
        throne_1.SetActive(false);
        windows_all_1.SetActive(false);
        bookshelves_1.SetActive(false);
    }
    void upgradeCarpet()
    {
        carpet_0.SetActive(false);
        carpet_1.SetActive(true);
    }

    void upgradeThrone()
    {
        throne_0.SetActive(false);
        throne_1.SetActive(true);
    }

    void upgradeWindows()
    {
        windows_all_0.SetActive(false);
        windows_all_1.SetActive(true);
    }

    void upgradeBookshelves()
    {
        bookshelves_0.SetActive(false);
        bookshelves_1.SetActive(true);
    }

}
