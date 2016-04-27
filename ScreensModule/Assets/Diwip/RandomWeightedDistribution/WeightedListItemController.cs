using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeightedListItemController : MonoBehaviour {
    public Image FillImage;
    public InputField InputField;
    private WeightedRandomDistributionViewPopulator _controller;

    public void Populate(WeightedRandomDistributionViewPopulator controller, int value, int totalValue)
    {
        this._controller = controller;

        FillImage.fillAmount = (float)value / (float)totalValue;
        InputField.text = value.ToString();
    }

    public void HandleEditEnd()
    {
        _controller.ItemEditComplete(this, Int32.Parse(InputField.text));
    }
}
