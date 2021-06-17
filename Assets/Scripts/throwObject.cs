using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter1
{
    public class throwObject : MonoBehaviour
    {
        public GameObject objectPrefab;
        private Transform myTransform;
        private float propulsionForce = 7;

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
            //GameObject go = (GameObject)Instantiate(objectPrefab, myTransform.TransformPoint(0, 0, 0.5f), Quaternion.Euler(myTransform.rotation.x, myTransform.rotation.y+30f, myTransform.rotation.z));
            go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.VelocityChange);
        }
    }

}
