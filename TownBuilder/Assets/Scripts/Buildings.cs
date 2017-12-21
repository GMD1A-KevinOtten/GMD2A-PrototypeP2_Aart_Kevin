using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour{

    public enum BuildingState
    {
        PrePlace,
        Constructing,
        Done
    }

    public BuildingState bs;
    //Building Var
    public int stoneNeeded;
    public int woodNeeded;
    public int MetalNeeded;
    public int health;
    public int maxHealth;

    public void KindaUpdate()
    {
        if(bs == BuildingState.PrePlace)
        {
            FollowMouse();
            PlaceBuilding();
            Rotate();
        }
    }

    public void Rotate()
    {
        if(Input.GetButton("Q"))
        {
            transform.Rotate(new Vector3(0, -2, 0));
        }
        if(Input.GetButton("E"))
        {
            transform.Rotate(new Vector3(0, 2, 0));
        }
    }

    public void FollowMouse()
    {
        if(bs == BuildingState.PrePlace)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                transform.position = hit.point;
            }
        }
    }

    public void PlaceBuilding()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bs = BuildingState.Constructing;
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
            bs = BuildingState.Done;
            print("JobDone");
        }
    }
}
