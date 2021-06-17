using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape

public class PauseM : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown("escape"))  
        {
            if (GameIsPaused) { Resume(); }
            else { Pause(); }
        }
    }
    public void Resume(){
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1f;
       GameIsPaused = false;
    }
    void Pause(){
       pauseMenuUI.SetActive(true);
       Time.timeScale = 0f;
       GameIsPaused = true;
    }
}