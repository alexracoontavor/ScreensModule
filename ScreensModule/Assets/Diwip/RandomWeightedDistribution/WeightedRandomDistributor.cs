using System;
using UnityEngine;

public class WeightedRandomDistributor
{
    int values;
    int[] _weights;

    int _totalWeight = 10000;

    public int TotalWeight
    {
        get
        {
            _totalWeight = 0;

            foreach (int i in _weights)
            {
                _totalWeight += i;
            }

            return _totalWeight;
        }
    }

    public int[] Weights
    {
        get
        {
            return _weights;
        }
    }

    public int HighestWeight
    {
        get
        {
            int highestWeight = 0;

            for (int i = 0; i < Weights.Length; i++)
            {
                if (Weights[i] > highestWeight)
                    highestWeight = Weights[i];
            }

            return highestWeight;
        }
    }

    public WeightedRandomDistributor(int values)
    {
        this.values = values;
        this._weights = new int[values];
        PopulateDefaultWeights();
    }

    private void PopulateDefaultWeights()
    {
        for (int i = 0; i < _weights.Length; i++)
        {
            _weights[i] = _totalWeight / _weights.Length;
        }    
    }

    public int GetRandomWeightedValue()
    {
        int result = 0, total = 0;
        int randVal = UnityEngine.Random.Range(0, TotalWeight + 1);

        for (result = 0; result < _weights.Length; result++)
        {
            total += _weights[result];
            if (total >= randVal) break;
        }

        if (result < 0 || result >= _weights.Length)
            Debug.Log("WTF?!");

        return result;
    }

    internal void RedistributeWeights(int index, int newValue)
    {
        _weights[index] = newValue;
    }
}
