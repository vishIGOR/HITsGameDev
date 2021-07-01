using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
 
[RequireComponent(typeof(Rigidbody))]

public class PlayerControl : MonoBehaviour
{

    public float normalSpeed = 2f;
    public float runningSpeed = 4f;

    public Transform head;
    public Transform body;

    public float sensitivity = 5f; // чувствительность мыши
    public float headMinY = -40f; // ограничение угла для головы
    public float headMaxY = 40f;

    public KeyCode jumpButton = KeyCode.Space;
    public KeyCode runningButton = KeyCode.LeftShift;// клавиша для прыжка
    public float jumpForce = 5f; // сила прыжка
    public float jumpDistance = 1.05f; // расстояние от центра объекта, до поверхности

    public Rigidbody mainRigidbody;

    private Vector3 worldRight = new Vector3(1, 0, 0);
    private Vector3 worldForward = new Vector3(0, 0, 1);
    private Vector3 worldUp = new Vector3(0, 1, 0);
    private Vector3 worldLeft = new Vector3(-1, 0, 0);
    private Vector3 worldBack = new Vector3(0, 0, -1);
    private Vector3 worldDown = new Vector3(0, -1, 0);
    private Scrollbar scr;

    private Vector3 directionRight;
    private Vector3 directionForward;
    private float currentSpeed;
    private float f, r;
    private int layerMask;
    private float rotationGorizontal;
    private float rotationVertical;

    private float rightVelocity;
    private float forwardVelocity;

    private Vector3 rightBorder;
    private Vector3 forwardBorder;

    public int isInGDA;
    public int GDADirection;
    //1 - âäîëü îñè y, ãîëîâà ïî îñè
    //2 - âäîëü îñè y, ãîëîâà ïðîòèâ îñè
    //3 - âäîëü îñè x, ãîëîâà ïî îñè
    //4 - âäîëü îñè x, ãîëîâà ïðîòèâ îñè
    //5 - âäîëü îñè z, ãîëîâà ïî îñè
    //6 - âäîëü îñè z, ãîëîâà ïðîòèâ îñè
    private float sinX;
    private float cosX;
    private float sinY;
    private float cosY;
    private float sinZ;
    private float cosZ;

    private float currentSin;
    private float currentCos;

    public bool inRotating = false;

    public float gorizontalAngle = 0;
    /*private float[] rotationByX = new float[7];
    private float[] rotationByY = new float[7];
    private float[] rotationByZ = new float[7];*/

    void Start()
    {

        directionForward = worldForward;
        directionRight = worldRight;
        isInGDA = 0;
        GDADirection = 1;
        mainRigidbody = GetComponent<Rigidbody>();
        mainRigidbody.freezeRotation = true;
        layerMask = 3;
    }

    void FixedUpdate()
    {
        Moving();
    }
    void Update()
    {
        CameraControl();
        Jumping();
    }
   public void setValue()
    {
        scr = GameObject.Find("ScrollbarSens").GetComponent<Scrollbar>();
        sensitivity = (int)Math.Round(scr.value*12.5); 
    } 

    private void CameraControl()
    {
        r = Input.GetAxis("Horizontal");
        f = Input.GetAxis("Vertical");

        rotationVertical += Input.GetAxis("Mouse Y") * sensitivity;
        rotationVertical = Mathf.Clamp(rotationVertical, headMinY, headMaxY);
        head.localEulerAngles = new Vector3(-rotationVertical, 0, 0);

        rotationGorizontal += Input.GetAxis("Mouse X") * sensitivity;
        body.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

        gorizontalAngle += Input.GetAxis("Mouse X") * sensitivity;
    }

    private void Moving()
    {
        if (Input.GetKey(runningButton) == true && isInAir() == false)
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        switch (GDADirection)
        {
            case 1:
                currentCos = Mathf.Cos(body.transform.rotation.eulerAngles.y * Mathf.PI / 180);
                currentSin = Mathf.Sin(body.transform.rotation.eulerAngles.y * Mathf.PI / 180);

                directionForward = worldForward;
                directionRight = worldRight;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.z);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.x);

                forwardBorder = new Vector3(0, 0, currentSpeed * currentCos * f + currentSpeed * -currentSin * r - mainRigidbody.velocity.z);
                rightBorder = new Vector3(currentSpeed * currentSin * f + currentSpeed * currentCos * r - mainRigidbody.velocity.x, 0, 0);
                break;
            case 2:
                r = -r;
                currentCos = Mathf.Cos(body.transform.rotation.eulerAngles.y * Mathf.PI / 180);
                currentSin = Mathf.Sin(body.transform.rotation.eulerAngles.y * Mathf.PI / 180);

                directionForward = worldForward;
                directionRight = worldRight;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.z);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.x);

                forwardBorder = new Vector3(0, 0, currentSpeed * currentCos * f + currentSpeed * -currentSin * r - mainRigidbody.velocity.z);
                rightBorder = new Vector3(currentSpeed * currentSin * f + currentSpeed * currentCos * r - mainRigidbody.velocity.x, 0, 0);
                break;
            case 3:
                r = -r;
                currentCos = Mathf.Cos(gorizontalAngle * Mathf.PI / 180);
                currentSin = Mathf.Sin(-gorizontalAngle * Mathf.PI / 180);

                directionForward = worldForward;
                directionRight = worldUp;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.z);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.y);

                forwardBorder = new Vector3(0, 0, currentSpeed * currentCos * f + currentSpeed * -currentSin * r - mainRigidbody.velocity.z);
                rightBorder = new Vector3(0, currentSpeed * currentSin * f + currentSpeed * currentCos * r - mainRigidbody.velocity.y, 0);
                break;
            case 4:

                currentCos = Mathf.Cos(gorizontalAngle * Mathf.PI / 180);
                currentSin = Mathf.Sin(gorizontalAngle * Mathf.PI / 180);

                directionForward = worldForward;
                directionRight = worldUp;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.z);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.y);

                forwardBorder = new Vector3(0, 0, currentSpeed * currentCos * f + currentSpeed * -currentSin * r - mainRigidbody.velocity.z);
                rightBorder = new Vector3(0, currentSpeed * currentSin * f + currentSpeed * currentCos * r - mainRigidbody.velocity.y, 0);
                break;
            case 5:
                currentCos = Mathf.Cos(gorizontalAngle * Mathf.PI / 180);
                currentSin = Mathf.Sin(gorizontalAngle * Mathf.PI / 180);

                directionForward = worldLeft;
                directionRight = worldDown;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.x);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.y);

                forwardBorder = new Vector3(-currentSpeed * currentCos * f - currentSpeed * -currentSin * r - mainRigidbody.velocity.x, 0, 0);
                rightBorder = new Vector3(0, -currentSpeed * currentSin * f - currentSpeed * currentCos * r - mainRigidbody.velocity.y, 0);
                break;
            case 6:
                currentCos = Mathf.Cos(gorizontalAngle * Mathf.PI / 180);
                currentSin = Mathf.Sin(gorizontalAngle * Mathf.PI / 180);

                directionForward = worldLeft;
                directionRight = worldUp;

                forwardVelocity = Mathf.Abs(mainRigidbody.velocity.x);
                rightVelocity = Mathf.Abs(mainRigidbody.velocity.y);

                forwardBorder = new Vector3(-currentSpeed * currentCos * f - currentSpeed * -currentSin * r - mainRigidbody.velocity.x, 0, 0);
                rightBorder = new Vector3(0, currentSpeed * currentSin * f + currentSpeed * currentCos * r - mainRigidbody.velocity.y, 0);
                break;
        }

        // спидометр)))
        /*Testing2(Mathf.Sqrt(mainRigidbody.velocity.x * mainRigidbody.velocity.x + mainRigidbody.velocity.z * mainRigidbody.velocity.z + mainRigidbody.velocity.y * mainRigidbody.velocity.y));*/

        mainRigidbody.AddForce(directionForward * currentSpeed * currentCos * f, ForceMode.VelocityChange);
        mainRigidbody.AddForce(directionForward * currentSpeed * -currentSin * r, ForceMode.VelocityChange);

        mainRigidbody.AddForce(directionRight * currentSpeed * currentSin * f, ForceMode.VelocityChange);
        mainRigidbody.AddForce(directionRight * currentSpeed * currentCos * r, ForceMode.VelocityChange);
        // Ограничение скорости, иначе объект будет постоянно ускоряться

        if (forwardVelocity - Mathf.Abs(currentSpeed * currentCos * f + currentSpeed * -currentSin * r) > 0)
        {
            if (forwardVelocity - Mathf.Abs(currentSpeed * currentCos * f + currentSpeed * -currentSin * r) < runningSpeed * 2.5)
            {
                mainRigidbody.velocity += forwardBorder;
            }
            else
            {
                mainRigidbody.AddForce(-directionForward * currentSpeed * currentCos * f, ForceMode.VelocityChange);
                mainRigidbody.AddForce(-directionForward * currentSpeed * -currentSin * r, ForceMode.VelocityChange);

            }
        }


        if (rightVelocity - Mathf.Abs(currentSpeed * currentSin * f + currentSpeed * currentCos * r) > 0)
        {

            if (rightVelocity - Mathf.Abs(currentSpeed * currentSin * f + currentSpeed * currentCos * r) < runningSpeed * 2.5)
            {
                mainRigidbody.velocity += rightBorder;
            }
            else
            {
                mainRigidbody.AddForce(-directionRight * currentSpeed * currentSin * f, ForceMode.VelocityChange);
                mainRigidbody.AddForce(-directionRight * currentSpeed * currentCos * r, ForceMode.VelocityChange);
            }
        }

    }
    private void Jumping()
    {
        if (Input.GetKeyDown(jumpButton) == false)
            return;


        if (isInAir() == false)
        {
            mainRigidbody.AddRelativeForce(worldUp * jumpForce, ForceMode.VelocityChange);

            if (isInGDA != 0)
                mainRigidbody.AddRelativeForce(worldUp * jumpForce * 0.5f, ForceMode.VelocityChange);
        }

    }

    private bool isInAir()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, jumpDistance, layerMask))
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);

    }

    private void Testing(float n)
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
            Debug.Log(n);
    }

    private void Testing2(float n)
    {
        Debug.Log(n);
    }
}