using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDistortionArtifact : MonoBehaviour
{
    private Collider gravityArea;
    private Rigidbody GDArb;
    private float gravityRadius = 25;

    private float xGravityForce;
    private float yGravityForce;
    private float zGravityForce;

    private bool isSetted = false;

    private float[] rotationByX = new float[7];
    private float[] rotationByY = new float[7];
    private float[] rotationByZ = new float[7];

    public int numberOfGDA;
    public int directionOfGDA;

    public PlayerControl player;

    //1 - ����� ��� y, ������ �� ���
    //2 - ����� ��� y, ������ ������ ���
    //3 - ����� ��� x, ������ �� ���
    //4 - ����� ��� x, ������ ������ ���
    //5 - ����� ��� z, ������ �� ���
    //6 - ����� ��� z, ������ ������ ���

    //�������� ��������������� ���������
    private const float forceValue = 0.1962F;
    private Vector3 zeroVelocity = new Vector3(0, 0, 0);

    private float rotationStartTime;
    private Vector3 previousAngles;
    private Vector3 newAngles;
    private bool isRotationActive = false;
    private float rotationDuration = 0.6f;

    void Start()
    {
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

        /////////////////////////////////////////////// ��� ������� ����� ������� ����� �������� ������� ������
        GDArb.velocity = new Vector3(0, 1f, 0);
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
                activationGDA();
            }
        }
        else
        {
            gravityDistortion(transform.position, gravityRadius);
        }
    }

    private void activationGDA()
    {
        //����� ����� ����� ���� ������


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
    private void gravityDistortion(Vector3 center, float radius)
    {

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var other in hitColliders)
        {
            if (other.attachedRigidbody)
            {
                if (other.gameObject == player.gameObject || other.gameObject == player.head.gameObject)
                {
                    if (player.isInGDA != numberOfGDA)
                        continue;
                }
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
            playerEnter();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isSetted == false)
            return;
        if (other.gameObject == player.gameObject)
        {
            playerExit();
            return;
        }
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().useGravity = true;

    }

    private void playerEnter()
    {
        gravityInversion(directionOfGDA, numberOfGDA);
    }

    private void playerExit()
    {

        if (player.isInGDA != numberOfGDA)
            return;

        gravityInversion(1, 0);
    }

    private void gravityInversion(int newDirection, int newNumber)
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
            Debug.Log("test");
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
