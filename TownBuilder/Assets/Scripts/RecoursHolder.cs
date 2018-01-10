using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoursHolder : MonoBehaviour {

    public Recourses rC;

    public void CollectRecources(Worker wk)
    {
        if(rC.food != 0)
        {
            wk.food += rC.food;
            rC.food = 0;
        }
        else if(rC.wood != 0)
        {
            wk.wood += rC.wood;
            rC.wood = 0;
        }
        else if(rC.stone != 0)
        {
            wk.stone += rC.stone;
            rC.stone = 0;
        }
        else if(rC.metal != 0)
        {
            wk.metal += rC.metal;
            rC.metal = 0;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
