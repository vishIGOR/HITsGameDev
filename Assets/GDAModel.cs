using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDAModel : MonoBehaviour
{
    public GameObject model;
    private Rigidbody GDArb;
    void Start()
    {
        GDArb = model.transform.parent.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter()
    {
        GDArb.isKinematic = true;
        model.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
    }
}
