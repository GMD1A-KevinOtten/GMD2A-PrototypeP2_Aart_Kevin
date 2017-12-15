using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public void Quit()
    {
        GameManager.QuitGame();
    }

    public void Play()
    {
        GameManager.SceneChange(1);
    }
}
