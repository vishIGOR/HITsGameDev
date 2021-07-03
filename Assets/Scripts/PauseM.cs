using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseM : MonoBehaviour
{
    static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject numOfArtifacts;
    public GameObject settingsMenuUI;
    PlayerControl contr;
    throwObject thr;
    public GameObject player;
    public GameObject playerHead;


    void Start()
    {
        contr = player.GetComponent<PlayerControl>();
        thr = playerHead.GetComponent<throwObject>();

    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GameIsPaused)
            {
                numOfArtifacts.SetActive(true);
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                Resume();
            }
            else
            {
                numOfArtifacts.SetActive(false);
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                Pause();
            }
            GameIsPaused = !GameIsPaused;

        }
    }
    public void LoadScene(int level)
    {
        GameIsPaused = !GameIsPaused;
        SceneManager.LoadScene(level);
    }
    public void ChangeSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    public void ReturnBack()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
    public void Resume()
    {
        contr.enabled = true;
        thr.enabled = true;
        Cursor.visible = false;
    }
    public void ContinueGame()
    {
        numOfArtifacts.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        contr.enabled = true;
        thr.enabled = true;
        Cursor.visible = false;
    }
    public void Pause()
    {
        contr.enabled = false;
        thr.enabled = false;
        Cursor.visible = true;
    }
}
