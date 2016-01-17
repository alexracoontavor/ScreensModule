using System;
using UnityEngine;
using UnityEngine.UI;

public class WeightedListTester : MonoBehaviour {

    public Text output;
    public WeightedRandomDistributionController controller;
    public int cycles = 10000;
    public InputField cyclesIF;

    public void Start()
    {
        cyclesIF.text = cycles.ToString();
    }

    public void UpdateCyclesInputField()
    {
        cycles = Int32.Parse(cyclesIF.text);
    }

    public void Test()
    {
        string s = cycles.ToString() + " cycles:\n\n";

        int[] outcomes = new int[controller.Distributor.Weights.Length];

        for (int i = 0; i < cycles; i++)
        {
            int val = controller.Distributor.GetRandomWeightedValue();

            outcomes[val]++;
        }

        for (int i = 0; i < outcomes.Length; i++)
        {
            s += "\n" + i.ToString() + ": " + outcomes[i].ToString();
        }

        output.text = s;
    }

}
