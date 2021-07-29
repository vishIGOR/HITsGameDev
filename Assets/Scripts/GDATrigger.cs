using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GDATrigger : MonoBehaviour
{
    public GameObject GDAModel;
    public GameObject GDAModel2;
    public GameObject Learn;
    public GameObject PressU;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.visible = true;
            PressU.SetActive(false);
            Destroy(GDAModel);
            Destroy(GDAModel2);
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

    public void Close()
    {
        Learn.SetActive(false);
        Cursor.visible = false;
    }


}
