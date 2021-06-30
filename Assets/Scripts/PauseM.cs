using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseM : MonoBehaviour
{
    static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    PlayerControl contr;
    throwObject thr;
    public GameObject player;
    public GameObject playerHead;

    void Start(){
        contr = player.GetComponent<PlayerControl>();
        thr = playerHead.GetComponent<throwObject>();

    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GameIsPaused) { Resume(); }
            else { Pause(); }
            GameIsPaused = !GameIsPaused;

        }
    }
    public void LoadScene(int level)
    {
        GameIsPaused = !GameIsPaused;
        SceneManager.LoadScene(level);

    }
    public void Resume()
    {
        contr.enabled = true;
        thr.enabled = true;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        contr.enabled = false;
        thr.enabled = false;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;

    }
}
