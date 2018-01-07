using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huis : Buildings {

    public override void Start()
    {
        base.Start();
        maxHealth = stoneNeeded + woodNeeded + MetalNeeded;
    }


    void Update ()
    {
        KindaUpdate();
	}
}
