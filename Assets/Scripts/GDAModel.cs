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

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "GDArtifact")
        {
            return;
        }
            
        GDArb.isKinematic = true;
        model.GetComponent<Rigidbody>().isKinematic = true;
    }

}