using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huis : Buildings {

	
	void Start ()
    {
        maxHealth = stoneNeeded + woodNeeded + MetalNeeded;
    }
	
	
	void Update ()
    {
        KindaUpdate();
	}
}
