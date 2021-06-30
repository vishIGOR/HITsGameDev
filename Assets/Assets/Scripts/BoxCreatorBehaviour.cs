using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreatorBehaviour : MonoBehaviour
{
    public bool isActive;

    public GameObject boxPrefab;
    public GameObject linkedBox;

    void Start()
    {
    }


    void Update()
    {
        if (isActive == true)
        {
            CreateBox();
            isActive = false;
        }

    }

    private void CreateBox()
    {
        if (linkedBox != null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        linkedBox = boxPrefab;
        Instantiate(linkedBox, transform);
        linkedBox.transform.localPosition = new Vector3(0, 0.5f, -3.2f);
    }
}
