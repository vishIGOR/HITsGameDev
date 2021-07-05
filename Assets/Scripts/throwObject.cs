using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwObject : MonoBehaviour
{
    public GameObject objectPrefab;
    public int numberOfArtifacts = 2;

    private Transform myTransform;
    private float propulsionForce = 10;

    private GameObject[] artifacts;

    private bool[] isArtifactSetted;

    // Start is called before the first frame update
    void Start()
    {
        artifacts = new GameObject[numberOfArtifacts];
        isArtifactSetted = new bool[numberOfArtifacts];

        for (int i = 0; i < numberOfArtifacts; ++i)
        {
            isArtifactSetted[i] = false;
        }
        SetInitialReference();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnObject(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnObject(1);
        }
    }
    void SetInitialReference()
    {
        myTransform = transform;
    }
    void SpawnObject(int currentNumber)
    {
        /* GameObject go = (GameObject)Instantiate(objectPrefab, myTransform.TransformPoint(0, 0, 0.5f), myTransform.rotation);
         go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.VelocityChange);*/

        if (isArtifactSetted[currentNumber] == false)
        {

            artifacts[currentNumber] = (GameObject)Instantiate(objectPrefab, myTransform.TransformPoint(0, 0, 0.5f), myTransform.rotation);
            artifacts[currentNumber].GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.VelocityChange);

            isArtifactSetted[currentNumber] = true;
        }
        else
        {
            Destroy(artifacts[currentNumber]);

            isArtifactSetted[currentNumber] = false;
        }
    }
}

