using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JobsAndNeedsManager : MonoBehaviour {

    public static List<GameObject> toBuild = new List<GameObject>();
    public static List<GameObject> toHarvest = new List<GameObject>();
    public static List<GameObject> toCollect = new List<GameObject>();
    public static List<GameObject> Storage = new List<GameObject>();
    public List<GameObject> selectedObjects = new List<GameObject>();
    public bool overButon;

    void Update()
    {
        SelectIntObject();
        print(Storage.Count);
    }

    public void OverButonTrigger()
    {
        if(overButon)
        {
            overButon = false;
        }
        else
        {
            overButon = true;
        }
    }

    public void SelectIntObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<HarvestableObjectHolder>())
                {
                    if(Input.GetButton("LShift"))
                    {
                        selectedObjects.Add(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    }
                    else
                    {
                        foreach (GameObject g in selectedObjects)
                        {
                            g.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                        }
                        selectedObjects.Clear();
                        selectedObjects.Add(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    }
                }
                else if (EventSystem.current.IsPointerOverGameObject() && overButon == false)
                {
                    foreach(GameObject gameobject in selectedObjects)
                    {
                        gameobject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    }
                    selectedObjects.Clear();
                }
            }
        }
    }

    public void AddToHarvestList()
    {
        foreach (GameObject gameobject in selectedObjects)
        {
            bool alreadyIn = false;
            foreach (GameObject harvestque in toHarvest)
            {
                if(harvestque == gameobject)
                {
                    alreadyIn = true;
                }
            }
            if(alreadyIn == false)
            {
                toHarvest.Add(gameobject);
            }
            gameobject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        }
        selectedObjects.Clear();
    }
}
