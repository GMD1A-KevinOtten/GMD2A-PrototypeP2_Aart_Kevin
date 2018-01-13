using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour {

    //BuildingPrefabs
    public static bool canBuild = true;

    public GameObject baseHouse;
    public GameObject baseStorageBarn;

    public void BuildBaseHouse()
    {
        if(canBuild == true)
        {
            Instantiate(baseHouse, new Vector3(0, 0, 0), Quaternion.identity);
            canBuild = false;
        }
    }

    public void BuildBaseStorageBarn()
    {
        if (canBuild == true)
        {
            Instantiate(baseStorageBarn, new Vector3(0, 0, 0), Quaternion.identity);
            canBuild = false;
        }
    }
}
