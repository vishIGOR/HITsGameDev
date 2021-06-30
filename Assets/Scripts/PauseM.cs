using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseM : MonoBehaviour
{
    static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
<<<<<<< HEAD
=======
    PlayerControl contr;
    throwObject thr;
    public GameObject player;
    public GameObject playerHead;

    void Start(){
        contr = player.GetComponent<PlayerControl>();
        thr = playerHead.GetComponent<throwObject>();

    }
>>>>>>> VishIGOR2
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GameIsPaused) { Resume(); }
            else { Pause(); }
            GameIsPaused = !GameIsPaused;

        }
    }
<<<<<<< HEAD
=======
    public void LoadScene(int level)
    {
        GameIsPaused = !GameIsPaused;
        SceneManager.LoadScene(level);

    }
>>>>>>> VishIGOR2
    public void Resume()
    {
        contr.enabled = true;
        thr.enabled = true;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
<<<<<<< HEAD
     void Pause()
    {
        pauseMenuUI.SetActive(true);
=======
    void Pause()
    {
        contr.enabled = false;
        thr.enabled = false;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
>>>>>>> VishIGOR2
        Time.timeScale = 0f;

    }
}
