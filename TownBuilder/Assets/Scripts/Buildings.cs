using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    public List<Transform> children = new List<Transform>();

    public virtual void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
    }

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
            int layerMask = 1 << 8;
            print("kk");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask) && hit.transform.tag == "Floor")
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
            if(children.Count > 2)
            {
                children[2].gameObject.SetActive(false);
                children[1].gameObject.SetActive(false);
            }
            else
            {
                children[1].gameObject.SetActive(false);
            }
            BuilderManager.canBuild = true;
        }
    }

    public void CheckProgresOnBuilding(Worker wk)
    {
        if(stoneNeeded != 0)
        {
            stoneNeeded += wk.stone;
            wk.stone = 0;
            if(stoneNeeded > 0)
            {
                wk.stone = stoneNeeded;
                stoneNeeded = 0;
            }
        }

        if (woodNeeded != 0)
        {
            woodNeeded += wk.wood;
            wk.wood = 0;
            if (woodNeeded > 0)
            {
                wk.wood = woodNeeded;
                woodNeeded = 0;
            }
        }

        if (MetalNeeded != 0)
        {
            MetalNeeded += wk.metal;
            wk.metal = 0;
            if (MetalNeeded > 0)
            {
                wk.metal = MetalNeeded;
                MetalNeeded = 0;
            }
        }

        else if(health != maxHealth)
        {
            health += 10;
            if (health == maxHealth / 2 && children.Count > 2)
            {
                children[1].gameObject.SetActive(false);
            }
        }
        else
        {
            if(children.Count >= 3)
            {
                children[2].gameObject.SetActive(false);
            }
            else
            {
                children[1].gameObject.SetActive(true);
            }
            bs = BuildingState.Done;
            print("JobDone");
        }
    }
}
