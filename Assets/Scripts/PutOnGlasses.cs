using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnGlasses : MonoBehaviour
{
    public GameObject glasses;
    public GameObject glassesOut;

    public GameObject bluePanel;
    public GameObject canv;
    PauseM pauseScript;

    private bool glassesAreOn = false;
    void Start()
    {
        pauseScript = canv.GetComponent<PauseM>();
    }
    IEnumerator OnCollisionCoroutine()
    {
        pauseScript.Pause();
        glasses.SetActive(true);
        glassesOut.SetActive(false);
        yield return new WaitForSeconds(0.9f);
        bluePanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (glassesAreOn) { Resume(); }
            else
            {
                StartCoroutine(OnCollisionCoroutine());
            }
            glassesAreOn = !glassesAreOn;

        }
    }
    private void Resume()
    {
        pauseScript.Resume();
        glasses.SetActive(false);
        glassesOut.SetActive(true);
        bluePanel.SetActive(false);
    }
}
