using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Items item;
    public Text nameText;
    public Text descriptionText;
    public Image iconImage;
    public Text attackPowerText;
    public Text defensePowerText;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = item.itemName;
        descriptionText.text = item.itemDescription;
        iconImage.sprite = item.itemIcon;
        attackPowerText.text = item.attackPower.ToString();
        defensePowerText.text = item.defensePower.ToString();
    }
}
