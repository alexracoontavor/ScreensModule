using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeightedListItemController : MonoBehaviour {
    public Image fillImage;
    public InputField inputField;
    WeightedRandomDistributionViewPopulator controller;

    int value, totalValue = 0;

    public void Populate(WeightedRandomDistributionViewPopulator controller, int value, int totalValue)
    {
        this.controller = controller;
        this.value = value;
        this.totalValue = totalValue;

        fillImage.fillAmount = (float)value / (float)totalValue;
        inputField.text = value.ToString();
    }

    public void HandleEditEnd()
    {
        controller.ItemEditComplete(this, Int32.Parse(inputField.text));
    }
}
