using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour{

    //Building Var
    public bool doneBuilding;
    public int stoneNeeded;
    public int woodNeeded;
    public int MetalNeeded;
    public int health;
    public int maxHealth;

    private void Start()
    {
        maxHealth = stoneNeeded + woodNeeded + MetalNeeded;
    }

    public void FollowMouse()
    {
        if(doneBuilding == false)
        {

        }
    }

    public void CheckProgresOnBuilding(Worker wk)
    {
        if(stoneNeeded != 0)
        {
            stoneNeeded -= wk.stone;
            if(stoneNeeded > 0)
            {
                wk.stone = stoneNeeded;
                stoneNeeded = 0;
            }
        }

        if (woodNeeded != 0)
        {
            woodNeeded -= wk.wood;
            if (woodNeeded > 0)
            {
                wk.wood = woodNeeded;
                woodNeeded = 0;
            }
        }

        if (MetalNeeded != 0)
        {
            MetalNeeded -= wk.metal;
            if (MetalNeeded > 0)
            {
                wk.metal = MetalNeeded;
                MetalNeeded = 0;
            }
        }

        else if(health != maxHealth)
        {
            health += 10;
        }
        else
        {
            doneBuilding = true;
            print("JobDone");
        }
    }
}
