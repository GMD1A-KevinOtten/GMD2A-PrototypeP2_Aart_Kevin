using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsAndNeedsManager : MonoBehaviour {

    public static List<GameObject> toBuild = new List<GameObject>();
    public static List<GameObject> toHarvest = new List<GameObject>();
    public List<GameObject> selectedObjects = new List<GameObject>();

    void Update()
    {
        SelectIntObject();
    }

    public void SelectIntObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<RecoursHolder>())
                {
                    if(Input.GetButton("LShift"))
                    {
                        print("ok");
                        selectedObjects.Add(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    }
                    else
                    {
                        foreach (GameObject gameobject in selectedObjects)
                        {
                            gameobject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                        }
                        selectedObjects.Clear();
                        selectedObjects.Add(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        print(selectedObjects);
                    }
                }
                else if(hit.transform.tag != "Canvas")
                {
                    foreach (GameObject gameobject in selectedObjects)
                    {
                        print("Wut");
                        gameobject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                        selectedObjects.Clear();
                    }
                }
            }
        }
    }

    public void AddToHarvestList()
    {
        foreach(GameObject gameobject in selectedObjects)
        {
            if(toHarvest.Count != 0)
            {
                foreach (GameObject harvestque in toHarvest)
                {
                    if (harvestque != gameobject)
                    {
                        toHarvest.Add(gameobject);
                        print(toHarvest[1]);
                    }
                }
            }
            else
            {
                toHarvest.Add(gameobject);
                print(toHarvest[0]);
            }
        }
        foreach (GameObject gameobject in selectedObjects)
        {
            gameobject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
