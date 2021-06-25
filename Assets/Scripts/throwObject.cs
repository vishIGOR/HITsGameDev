using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwObject : MonoBehaviour
    {
        public GameObject objectPrefab;
        private Transform myTransform;
        private float propulsionForce = 10;

        // Start is called before the first frame update
        void Start()
        {
            SetInitialReference();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnObject();
            }
        }
        void SetInitialReference()
        {
            myTransform = transform;
        }
        void SpawnObject()
        {
            GameObject go = (GameObject)Instantiate(objectPrefab, myTransform.TransformPoint(0, 0, 0.5f),myTransform.rotation);
            go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.VelocityChange);
        }
    }

