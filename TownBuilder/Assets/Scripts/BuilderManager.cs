using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour {

    //BuildingPrefabs
    public GameObject baseHouse;
    public GameObject baseStorageBarn;

    public void BuildBaseHouse()
    {
        Instantiate(baseHouse, Camera.main.ScreenToWorldPoint(Input.mousePosition - baseHouse.transform.position), Quaternion.identity);
    }

    public void BuildBaseStorageBarn()
    {
        Instantiate(baseStorageBarn, Input.mousePosition, Quaternion.identity);
    }
}
