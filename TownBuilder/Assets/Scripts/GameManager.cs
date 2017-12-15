using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
    }

    public static void PauseToggle()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void SceneChange(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}
