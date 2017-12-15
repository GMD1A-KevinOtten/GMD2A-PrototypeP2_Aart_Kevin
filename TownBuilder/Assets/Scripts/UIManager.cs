using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject basePannel;
    public GameObject pausePanel;

    private void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            UIToggle();
        }
    }

    public void UIToggle()
    {
        GameManager.PauseToggle();
        if(basePannel.activeSelf)
        {
            basePannel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            basePannel.SetActive(true);
        }
        GameManager.PauseToggle();
    }

    public void Resume()
    {
        UIToggle();
    }

    public void Menu()
    {
        Time.timeScale = 1;
        GameManager.SceneChange(0);
    }
}
