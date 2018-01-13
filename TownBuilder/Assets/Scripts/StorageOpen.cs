using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageOpen : Buildings
{

    public int woodStorage;
    public int stoneStorage;
    public int metalStorage;
    public int foodStorage;
    public int maxStorage;

    public override void Start()
    {
        base.Start();
        maxHealth = stoneNeeded + woodNeeded + MetalNeeded;
    }


    void Update()
    {
        KindaUpdate();
        if (health == maxHealth && bs == BuildingState.Done)
        {
            JobsAndNeedsManager.Storage.Add(this.gameObject);
        }
    }
}