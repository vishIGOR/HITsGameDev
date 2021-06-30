using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GravityDistortionArtifact : MonoBehaviour
{
    private Collider gravityArea;
    private Rigidbody GDArb;
    public float gravityRadius = 2;

    private float xGravityForce;
    private float yGravityForce;
    private float zGravityForce;

    private bool isSetted = false;

    private float[] rotationByX = new float[7];
    private float[] rotationByY = new float[7];
    private float[] rotationByZ = new float[7];
    private InputField inputNum;

    public int numberOfGDA;

    public int directionOfGDA = 1;

    private PlayerControl player;
    private Scrollbar scr;

    //1 - вдоль оси y, голова по оси
    //2 - вдоль оси y, голова против оси
    //3 - вдоль оси x, голова по оси
    //4 - вдоль оси x, голова против оси
    //5 - вдоль оси z, голова по оси
    //6 - вдоль оси z, голова против оси

    //значение гравитационного ускорения
    private const float forceValue = 0.1962F;
    private Vector3 zeroVelocity = new Vector3(0, 0, 0);

    private float rotationStartTime;
    private Vector3 previousAngles;
    private Vector3 newAngles;
    private bool isRotationActive = false;
    private float rotationDuration = 0.6f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        // inputNum = GameObject.Find("InputField").GetComponent<InputField>();
        rotationByX[1] = 0;
        rotationByY[1] = 0;
        rotationByZ[1] = 0;

        rotationByX[2] = -180;
        rotationByY[2] = 180;
        rotationByZ[2] = 0;

        rotationByX[3] = 0;
        rotationByY[3] = 0;
        rotationByZ[3] = -90;

        rotationByX[4] = 0;
        rotationByY[4] = 0;
        rotationByZ[4] = 90;

        rotationByX[5] = 0;
        rotationByY[5] = -90;
        rotationByZ[5] = -90;

        rotationByX[6] = 0;
        rotationByY[6] = -90;
        rotationByZ[6] = 90;

        gravityArea = GetComponent<Collider>();
        gravityArea.isTrigger = true;


        GDArb = GetComponent<Rigidbody>();

        /////////////////////////////////////////////// эту строчку нужно удалить после создания функции броска
        //GDArb.velocity = new Vector3(0, 1f, 0);
    }
    void Update()
    {
        PlayerRotation(isRotationActive);
    }

    void FixedUpdate()
    {
        if (isSetted == false)
        {
            if (GDArb.isKinematic == true)
            {
                GDArb.velocity = zeroVelocity;
                isSetted = true;
                ActivationGDA();
            }
        }
        else
        {
            GravityDistortion(transform.position, gravityRadius);
        }
    }

<<<<<<< HEAD
    public void setVale()
    {
        scr = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        directionOfGDA = (int)Math.Round(scr.value*5+1); 
    }
    private void activationGDA()
    {
=======
    public void SetValue()
    {
        scr = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        directionOfGDA = (int)Math.Round(scr.value*5+1); 
    }
    private void ActivationGDA()
    {
>>>>>>> VishIGOR2
        //потом здесь будет ввод данных
        

        switch (directionOfGDA)
        {
            case 1:
                xGravityForce = 0;
                yGravityForce = -forceValue;
                zGravityForce = 0;
                break;
            case 2:
                xGravityForce = 0;
                yGravityForce = forceValue;
                zGravityForce = 0;
                break;
            case 3:
                xGravityForce = -forceValue;
                yGravityForce = 0;
                zGravityForce = 0;
                break;
            case 4: 
                xGravityForce = forceValue;
                yGravityForce = 0;
                zGravityForce = 0;
                break;
            case 5:
                xGravityForce = 0;
                yGravityForce = 0;
                zGravityForce = -forceValue;
                break;
            case 6:
                xGravityForce = 0;
                yGravityForce = 0;
                zGravityForce = forceValue;
                break;
        }
    }
    private void GravityDistortion(Vector3 center, float radius)
    {

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var other in hitColliders)
        {
            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.useGravity = false;
                other.attachedRigidbody.AddForce(xGravityForce, yGravityForce, zGravityForce, ForceMode.VelocityChange);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSetted == false)
            return;

        if (other.gameObject == player.gameObject)
        {
            PlayerEnter();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isSetted == false)
            return;
        if (other.gameObject == player.gameObject)
        {
            PlayerExit();
            return;
        }
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().useGravity = true;

    }

    private void PlayerEnter()
    {
        GravityInversion(directionOfGDA, numberOfGDA);
    }

    private void PlayerExit()
    {

        if (player.isInGDA != numberOfGDA)
            return;

        GravityInversion(1, 0);
    }

    private void GravityInversion(int newDirection, int newNumber)
    {
        int previousDirection = player.GDADirection;
        player.GDADirection = newDirection;
        player.isInGDA = newNumber;

        float previousGorizontalAngle = player.gorizontalAngle;
        /*body.localEulerAngles = new Vector3(body.transform.rotation.x, rotationGorizontal, body.transform.rotation.z);*/

        previousAngles = player.transform.eulerAngles;

        player.inRotating = true;
        player.transform.eulerAngles = new Vector3(rotationByX[newDirection], rotationByY[newDirection], rotationByZ[newDirection]);

        if (Mathf.Abs(newDirection / 2 - previousDirection / 2) == 1)
        {
            player.body.Rotate(0, -player.gorizontalAngle, 0);
            player.gorizontalAngle = -player.gorizontalAngle;
        }
        else
        {
            player.body.Rotate(0, player.gorizontalAngle, 0);
        }


        newAngles = player.transform.eulerAngles;
        player.transform.eulerAngles = previousAngles;
        isRotationActive = true;
        rotationStartTime = -42f;

        if (Mathf.Abs(newAngles.x - previousAngles.x) > 180)
        {
            if (previousAngles.x < 0)
            {
                previousAngles.x += 360;
            }
            else
            {
                newAngles.x += 360;
            }
        }
        if (Mathf.Abs(newAngles.y - previousAngles.y) > 180)
        {
            if (previousAngles.y < 0)
            {
                previousAngles.y += 360;
            }
            else
            {
                newAngles.y += 360;
            }
        }
        if (Mathf.Abs(newAngles.z - previousAngles.z) > 180)
        {
            if (previousAngles.z < 0)
            {
                previousAngles.z += 360;
            }
            else
            {
                newAngles.z += 360;
            }
        }

        if (newNumber != 0)
            player.GetComponent<Rigidbody>().useGravity = false;
        else
            player.GetComponent<Rigidbody>().useGravity = true;
    }

    private void PlayerRotation(bool isActive)
    {
        if (!isActive)
            return;

        if (rotationStartTime == -42f)
        {
            rotationStartTime = Time.time;
        }

        if (Time.time - rotationStartTime >= rotationDuration)
        {
            isRotationActive = false;
            return;
        }

        float t = (Time.time - rotationStartTime) / rotationDuration;
        Vector3 currentAngles;

        
        currentAngles.x = Mathf.SmoothStep(previousAngles.x, newAngles.x, t);
        currentAngles.y = Mathf.SmoothStep(previousAngles.y, newAngles.y, t);
        currentAngles.z = Mathf.SmoothStep(previousAngles.z, newAngles.z, t);

        player.transform.eulerAngles = currentAngles;
    }
}
