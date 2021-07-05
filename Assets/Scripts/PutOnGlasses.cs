using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PutOnGlasses : MonoBehaviour
{
    public GameObject glasses;
    public GameObject glassesOut;

    public GameObject bluePanel;
    public GameObject canv;
    PauseM pauseScript;

    public GameObject particlePrefab;

    private GameObject player;
    private GameObject particle;
    private GameObject slider;

    private int direction;


    private float[] rotationByX = new float[7];
    private float[] rotationByY = new float[7];
    private float[] rotationByZ = new float[7];

    private bool glassesAreOn = false;
    void Start()
    {
        rotationByX[1] = 0;
        rotationByY[1] = 0;
        rotationByZ[1] = 0;

        rotationByX[2] = -180;
        rotationByY[2] = 180;
        rotationByZ[2] = 0;

        rotationByX[3] = 0;
        rotationByY[3] = 0;
        rotationByZ[3] = -90;

        rotationByX[4] = 0;
        rotationByY[4] = 0;
        rotationByZ[4] = 90;

        rotationByX[5] = 0;
        rotationByY[5] = -90;
        rotationByZ[5] = -90;

        rotationByX[6] = 0;
        rotationByY[6] = -90;
        rotationByZ[6] = 90;

        slider = GameObject.Find("Slider");
        player = GameObject.Find("Player");
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
            usingParticles();

            if (glassesAreOn) { Resume(); }
            else
            {
                StartCoroutine(OnCollisionCoroutine());
            }
            glassesAreOn = !glassesAreOn;

        }

        updateParticles();

    }
    private void Resume()
    {
        pauseScript.Resume();
        glasses.SetActive(false);
        glassesOut.SetActive(true);
        bluePanel.SetActive(false);
    }

    private void usingParticles()
    {
        if (glassesAreOn == true)
        {
            Destroy(particle);
            return;
        }

        particle = (GameObject)Instantiate(particlePrefab);

        particle.transform.parent = player.GetComponent<PlayerControl>().head;
        particle.transform.localPosition = new Vector3(0, 0, 0.6f);

        /*particle.transform.parent = null;*/
    }

    private void updateParticles()
    {
        if (glassesAreOn == false)
        {
            return; 
        }
        else
        {
            if (slider == null)
            {
                if(GameObject.Find("Slider") == null)
                {
                    return;
                }
                else
                {
                    slider = GameObject.Find("Slider");
                }
            }
        }

        direction = (int)Math.Round(slider.GetComponent<Slider>().value);

        particle.transform.eulerAngles = new Vector3(rotationByX[direction], rotationByY[direction], rotationByZ[direction]);
    }

}
