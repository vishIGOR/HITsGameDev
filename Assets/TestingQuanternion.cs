using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingQuanternion : MonoBehaviour
{
    // Minimum and maximum values for the transition.
    float minimum = 409f;
    float maximum = 420f;

    // Time taken for the transition.
    float duration = 5.0f;

    float startTime;

    void Start()
    {
        // Make a note of the time the script started.
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the fraction of the total duration that has passed.
        float t = (Time.time - startTime) / duration;
        transform.position = new Vector3(Mathf.SmoothStep(minimum, maximum, t), 245, 450);
    }
    public Transform body;
    /*void Start()
    {

    }


    void Update()
    {
        Rotating();
    }

    void FixedUpdate()
    {

    }

    private void Rotating()
    {

        *//*if (Input.GetKey(KeyCode.Alpha1))
        {
            body.RotateAround(body.right, 0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            body.RotateAround(body.right, -0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            body.RotateAround(body.up, 0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            body.RotateAround(body.up, -0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            body.RotateAround(body.forward, 0.01f);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            body.RotateAround(body.forward, -0.01f);
        }*//*

        if (Input.GetKey(KeyCode.Alpha1))
        {
            body.Rotate(0, 0, 1f);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            body.Rotate(0, 0, -1f);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            body.Rotate(0, 1f, 0);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            body.Rotate(0, -1f, 0);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            body.Rotate(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            body.Rotate(-1f, 0, 0);
        }
    }*/
}
