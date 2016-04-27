using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightedRandomDistributionViewPopulator : MonoBehaviour {

    public InputField InputField;
    public WeightedRandomDistributionController Controller;
    public GameObject Item;
    public Transform ListRoot;

    private List<WeightedListItemController> _items;

    private bool _isWaitingToRepopulate;

    // Use this for initialization
    private void Start() {
        _items = new List<WeightedListItemController>();
        Repopulate();
    }

    public void EditComplete()
    {
        Controller.Repopulate(Int32.Parse(InputField.text));
    }

    public void Repopulate()
    {
        _items.Clear();

        InputField.text = Controller.NumValues.ToString();

        int children = ListRoot.childCount;

        for (int i = 0; i < children; i++)
        {
            DestroyImmediate(ListRoot.GetChild(0).gameObject);
        }

        for (int i = 0; i < Controller.NumValues; i++)
        {
            GameObject go = Instantiate(Item);
            go.transform.SetParent(ListRoot);
            _items.Add(go.GetComponent<WeightedListItemController>());
        }

        int[] weights = Controller.Distributor.Weights;

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Populate(this, weights[i], Controller.Distributor.HighestWeight);
        }
    }

    public void ItemEditComplete(WeightedListItemController item, int newValue)
    {
        Controller.Distributor.RedistributeWeights(_items.IndexOf(item), newValue);
        _isWaitingToRepopulate = true;
    }

    private void Update()
    {
        if (_isWaitingToRepopulate)
        {
            _isWaitingToRepopulate = false;
            Repopulate();
        }
    }
}
