using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    private GameObject panel;


    public int pressedObjectsCounter;
    private bool buttonPosition;

    public DoorBehaviour linkedDoor;
    public BoxCreatorBehaviour linkedBoxCreator;

    private Vector3 previousPanelPosition;
    private Vector3 newPanelPosition;
    private float startTime;
    private float durationTime = 0.7f;

    void Start()
    {
        panel = transform.Find("TopPanel").gameObject;

        pressedObjectsCounter = 0;
        buttonPosition = false;

        if (linkedDoor != null)
        {
            linkedDoor.isActive = false;
        }

        if (linkedBoxCreator != null)
        {
            linkedBoxCreator.isActive = false;
        }
    }

    void Update()
    {
        if (buttonPosition == true)
        {
            MoveButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {

            pressedObjectsCounter++;
            if(pressedObjectsCounter == 1)
            {
                buttonPosition = true;

                if (linkedDoor != null)
                {
                    linkedDoor.isActive = true;
                }

                if (linkedBoxCreator != null)
                {
                    linkedBoxCreator.isActive = true;
                }
            }

            previousPanelPosition = panel.transform.localPosition;

            newPanelPosition = new Vector3(-0.85f, -0.4f, 0.85f);

            startTime = -42f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {

            pressedObjectsCounter--;
            if (pressedObjectsCounter == 0)
            {
                buttonPosition = true;

                if (linkedDoor != null)
                {
                    linkedDoor.isActive = false;
                }

                if (linkedBoxCreator != null)
                {
                    linkedBoxCreator.isActive = false;
                }
            }

            previousPanelPosition = panel.transform.localPosition;

            newPanelPosition = new Vector3(-0.85f, -0.125f, 0.85f);

            startTime = -42f;
        }
    }

    private void MoveButton()
    {
        if (startTime == -42f)
        {
            startTime = Time.time;
        }

        if (Time.time - startTime >= durationTime)
        {
            buttonPosition = false;
            return;
        }

        float t = (Time.time - startTime) / durationTime;
        Vector3 currentPanelPosition;

        currentPanelPosition.x = Mathf.SmoothStep(previousPanelPosition.x, newPanelPosition.x, t);
        currentPanelPosition.y = Mathf.SmoothStep(previousPanelPosition.y, newPanelPosition.y, t);
        currentPanelPosition.z = Mathf.SmoothStep(previousPanelPosition.z, newPanelPosition.z, t);


        panel.transform.localPosition = currentPanelPosition;
    }
}
