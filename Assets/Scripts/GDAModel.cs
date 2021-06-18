using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDAModel : MonoBehaviour
{
    private GameObject model;
    private Rigidbody GDArb;
    private float speed;
    void Start()
    {
        model = transform.gameObject;
        GDArb = model.transform.parent.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter()
    {
        GDArb.isKinematic = true;
        model.GetComponent<Rigidbody>().isKinematic = true;
    }

}