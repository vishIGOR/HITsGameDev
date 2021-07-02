using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class throwObject : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject message;

    public int maximumOfArtifacts;

    private Transform myTransform;
    private int numOfArtifacts;
    private Text numOfArtifactsText;
    private float propulsionForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        numOfArtifactsText = GameObject.Find("NumOfArtifacts").GetComponent<Text>();
        numOfArtifactsText.text = maximumOfArtifacts.ToString();
        SetInitialReference();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            numOfArtifacts = Convert.ToInt32(numOfArtifactsText.text);
            if (numOfArtifacts != 0)
            {
                SpawnObject();
                numOfArtifacts--;
                numOfArtifactsText.text = numOfArtifacts.ToString();
            }
            else
            {
                StartCoroutine(OnCollisionCoroutine());
            }
        }
    }
    IEnumerator OnCollisionCoroutine()
    {
        message.SetActive(true);
        yield return new WaitForSeconds(1f);
        message.SetActive(false);

    }
    void SetInitialReference()
    {
        myTransform = transform;
    }
    void SpawnObject()
    {
        GameObject go = (GameObject)Instantiate(objectPrefab, myTransform.TransformPoint(0, 0, 0.5f), myTransform.rotation);
        go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.VelocityChange);
    }
}

