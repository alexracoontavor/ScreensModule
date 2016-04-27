using UnityEngine;

public class WeightedRandomDistributionController : MonoBehaviour {
    private WeightedRandomDistributor _distributor;
    public WeightedRandomDistributionViewPopulator View;

    public int NumValues = 10;

    public WeightedRandomDistributor Distributor
    {
        get
        {
            return _distributor;
        }

        set
        {
            _distributor = value;
        }
    }

    // Use this for initialization
    private void Start () {
        _distributor = new WeightedRandomDistributor(NumValues);
	}
	
	public void Repopulate(int numValues)
    {
        if (this.NumValues != numValues)
        {
            this.NumValues = numValues;
            _distributor = new WeightedRandomDistributor(numValues);
            View.Repopulate();
        }
    }
}
