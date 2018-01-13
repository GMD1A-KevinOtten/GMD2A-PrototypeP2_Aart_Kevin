using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huis : Buildings {

    public override void Start()
    {
        base.Start();
        maxHealth = stoneNeeded + woodNeeded + MetalNeeded;
        if(maxHealth == 0)
        {
            maxHealth = 100;
        }
    }


    void Update ()
    {
        KindaUpdate();
	}
}
