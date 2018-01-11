using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObjectHolder : MonoBehaviour
{
    public HarvestableObjects hO;
    public int harvestProgress;

    private void OnDestroy()
    {
        JobsAndNeedsManager.toHarvest.Remove(gameObject);
    }
}
