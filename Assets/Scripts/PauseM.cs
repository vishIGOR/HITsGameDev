using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseM : MonoBehaviour
{
    static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GameIsPaused = !GameIsPaused;
            if (GameIsPaused) { Resume(); }
            else { Pause(); }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
     void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }
}
