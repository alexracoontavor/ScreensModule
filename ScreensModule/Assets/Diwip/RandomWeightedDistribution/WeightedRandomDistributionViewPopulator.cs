using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightedRandomDistributionViewPopulator : MonoBehaviour {

    public InputField inputField;
    public WeightedRandomDistributionController controller;
    public GameObject item;
    public Transform listRoot;

    List<WeightedListItemController> items;

    bool isWaitingToRepopulate;

    // Use this for initialization
    void Start() {
        items = new List<WeightedListItemController>();
        Repopulate();
    }

    public void EditComplete()
    {
        controller.Repopulate(Int32.Parse(inputField.text));
    }

    public void Repopulate()
    {
        items.Clear();

        inputField.text = controller.numValues.ToString();

        int children = listRoot.childCount;

        for (int i = 0; i < children; i++)
        {
            DestroyImmediate(listRoot.GetChild(0).gameObject);
        }

        for (int i = 0; i < controller.numValues; i++)
        {
            GameObject go = Instantiate(item);
            go.transform.SetParent(listRoot);
            items.Add(go.GetComponent<WeightedListItemController>());
        }

        int[] weights = controller.Distributor.Weights;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].Populate(this, weights[i], controller.Distributor.HighestWeight);
        }
    }

    public void ItemEditComplete(WeightedListItemController item, int newValue)
    {
        controller.Distributor.RedistributeWeights(items.IndexOf(item), newValue);
        isWaitingToRepopulate = true;
    }

    void Update()
    {
        if (isWaitingToRepopulate)
        {
            isWaitingToRepopulate = false;
            Repopulate();
        }
    }
}
