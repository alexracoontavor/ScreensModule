using System;
using UnityEngine;
using UnityEngine.UI;

public class WeightedListTester : MonoBehaviour {

    public Text Output;
    public WeightedRandomDistributionController Controller;
    public int Cycles = 10000;
    public InputField CyclesIf;

    public void Start()
    {
        CyclesIf.text = Cycles.ToString();
    }

    public void UpdateCyclesInputField()
    {
        Cycles = Int32.Parse(CyclesIf.text);
    }

    public void Test()
    {
        string s = Cycles.ToString() + " cycles:\n\n";

        int[] outcomes = new int[Controller.Distributor.Weights.Length];

        for (int i = 0; i < Cycles; i++)
        {
            int val = Controller.Distributor.GetRandomWeightedValue();

            outcomes[val]++;
        }

        for (int i = 0; i < outcomes.Length; i++)
        {
            s += "\n" + i.ToString() + ": " + outcomes[i].ToString();
        }

        Output.text = s;
    }

}
