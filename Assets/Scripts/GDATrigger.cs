using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GDATrigger : MonoBehaviour
{
    public GameObject GDAModel;
    public GameObject Learn;
    public GameObject num0;
    public GameObject num1;
    Text numOfMaxArtText;
    public GameObject PressU;
    void Start()
    {
        numOfMaxArtText = num1.GetComponent<Text>();
        numOfMaxArtText.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.visible = true;
            numOfMaxArtText.enabled = true;
            num0.SetActive(false);
            PressU.SetActive(false);
            Destroy(GDAModel);
            Learn.SetActive(true);

        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PressU.SetActive(true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            PressU.SetActive(false);
        }
    }

}
