using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour {

    //BuildingPrefabs
    public GameObject baseHouse;
    public GameObject baseStorageBarn;

    public void BuildBaseHouse()
    {
        Instantiate(baseHouse, new Vector3(0,0,0), Quaternion.identity);
    }

    public void BuildBaseStorageBarn()
    {
        Instantiate(baseStorageBarn, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
