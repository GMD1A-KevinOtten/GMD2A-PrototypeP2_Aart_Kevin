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
            print("Ok");
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
