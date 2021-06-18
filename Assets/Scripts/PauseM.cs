using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

// Quits the player when the user hits escape

public class PauseM : MonoBehaviour
{
    static bool GameIsPaused = false;
    [SerializeField]
    public GameObject pauseMenuUI;
    [SerializeField]
    private GameObject player;
    FirstPersonController controller;
    void Start()
    {
        controller = player.GetComponent<FirstPersonController>();
    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GameIsPaused = !GameIsPaused;
            if (GameIsPaused) { Resume(); }
            else { Pause(); }
        }
    }
    void Resume()
    {
        pauseMenuUI.SetActive(true);
        controller.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(false);
        controller.enabled = true;
        Time.timeScale = 1f;

    }
}
