using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageOpen : Buildings
{
    public bool inList;
    public int woodStorage;
    public int stoneStorage;
    public int metalStorage;
    public int foodStorage;
    public int maxStorage;

    public override void Start()
    {
        base.Start();
        maxHealth = stoneNeeded + woodNeeded + metalNeeded;
        if (maxHealth == 0)
        {
            maxHealth = 30;
        }
    }


    void Update()
    {
        KindaUpdate();
        if (health == maxHealth && bs == BuildingState.Done && inList == false)
        {
            inList = true;
            JobsAndNeedsManager.Storage.Add(this.gameObject);
        }
    }
}