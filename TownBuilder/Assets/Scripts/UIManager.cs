using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //Pannels
    public GameObject basePannel;
    public GameObject pausePanel;
    //SubPannels
    public GameObject BuildingListPannels;
    // Script Refrences
    public BuilderManager bM;
    

    private void Start()
    {
        UIToggle();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            UIToggle();
        }
    }

    public void UIToggle()
    {
        if(basePannel.activeSelf)
        {
            basePannel.SetActive(false);
            pausePanel.SetActive(true);
            GameManager.PauseToggle();
        }
        else if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            basePannel.SetActive(true);
            GameManager.PauseToggle();
        }
        else
        {
            basePannel.SetActive(true);
        }
    }


    //PauseMenu
    public void Resume()
    {
        UIToggle();
    }

    public void Menu()
    {
        Time.timeScale = 1;
        GameManager.SceneChange(0);
    }
    //BuilderButtons
    public void BaseHouse()
    {
        bM.BuildBaseHouse();
    }

    public void BaseStorageBarn()
    {
        bM.BuildBaseStorageBarn();
    }

    public void ToggleBuildingList()
    {
        if(BuildingListPannels.activeSelf)
        {
            BuildingListPannels.SetActive(false);
        }
        else if(!BuildingListPannels.activeSelf)
        {
            BuildingListPannels.SetActive(true);
        }
    }
}
