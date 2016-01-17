using UnityEngine;

public class WeightedRandomDistributionController : MonoBehaviour {

    WeightedRandomDistributor distributor;
    public WeightedRandomDistributionViewPopulator view;

    public int numValues = 10;

    public WeightedRandomDistributor Distributor
    {
        get
        {
            return distributor;
        }

        set
        {
            distributor = value;
        }
    }

    // Use this for initialization
    void Start () {
        distributor = new WeightedRandomDistributor(numValues);
	}
	
	public void Repopulate(int numValues)
    {
        if (this.numValues != numValues)
        {
            this.numValues = numValues;
            distributor = new WeightedRandomDistributor(numValues);
            view.Repopulate();
        }
    }
}
