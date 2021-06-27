using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private PlayerControl player;
    private GameObject leftDoor;
    private GameObject rightDoor;

    public bool isActive;

    private bool doorsPosition;

    private Vector3 previousLeftDoorPosition;
    private Vector3 newLeftDoorPosition;
    private Vector3 previousRightDoorPosition;
    private Vector3 newRightDoorPosition;
    private float startTime;
    private float durationTime = 0.6f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        leftDoor = transform.Find("LeftDoor").gameObject;
        rightDoor = transform.Find("RightDoor").gameObject;

        isActive = true;
        doorsPosition = false;
    }


    void Update()
    {
        if (isActive == true)
        {
            if (doorsPosition == true)
            {
                MoveDoors();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            doorsPosition = true;

            previousLeftDoorPosition = leftDoor.transform.localPosition;
            previousRightDoorPosition = rightDoor.transform.localPosition;

            newLeftDoorPosition = new Vector3(-4, -2, 0);
            newRightDoorPosition = new Vector3(2, -2, 0);

            startTime = -42f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            doorsPosition = true;

            previousLeftDoorPosition = leftDoor.transform.localPosition;
            previousRightDoorPosition = rightDoor.transform.localPosition;

            newLeftDoorPosition = new Vector3(-2, -2, 0);
            newRightDoorPosition = new Vector3(0, -2, 0);

            startTime = -42f;
        }
    }


    private void MoveDoors()
    {
        if (startTime == -42f)
        {
            startTime = Time.time;
        }

        if (Time.time - startTime >= durationTime)
        {
            doorsPosition = false;
            return;
        }

        float t = (Time.time - startTime) / durationTime;
        Vector3 currentLeftDoorPosition;
        Vector3 currentRightDoorPosition;

        currentLeftDoorPosition.x = Mathf.SmoothStep(previousLeftDoorPosition.x, newLeftDoorPosition.x, t);
        currentLeftDoorPosition.y = Mathf.SmoothStep(previousLeftDoorPosition.y, newLeftDoorPosition.y, t);
        currentLeftDoorPosition.z = Mathf.SmoothStep(previousLeftDoorPosition.z, newLeftDoorPosition.z, t);

        currentRightDoorPosition.x = Mathf.SmoothStep(previousRightDoorPosition.x, newRightDoorPosition.x, t);
        currentRightDoorPosition.y = Mathf.SmoothStep(previousRightDoorPosition.y, newRightDoorPosition.y, t);
        currentRightDoorPosition.z = Mathf.SmoothStep(previousRightDoorPosition.z, newRightDoorPosition.z, t);

        leftDoor.transform.localPosition = currentLeftDoorPosition;
        rightDoor.transform.localPosition = currentRightDoorPosition;
    }

}
