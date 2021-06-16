using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDistortionArtifact : MonoBehaviour
{
    private Collider gravityArea;
    private Rigidbody GDArb;
    private float gravityRadius= 25;

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

    //1 - вдоль оси y, голова по оси
    //2 - вдоль оси y, голова против оси
    //3 - вдоль оси x, голова по оси
    //4 - вдоль оси x, голова против оси
    //5 - вдоль оси z, голова по оси
    //6 - вдоль оси z, голова против оси

    //значение гравитационного ускорения
    private const float forceValue = 0.1962F;
    private Vector3 zeroVelocity = new Vector3(0, 0, 0);
    void Start()
    {
        rotationByX[1] = 0;
        rotationByY[1] = 0;
        rotationByZ[1] = 0;

        rotationByX[2] = -180;
        rotationByY[2] = 0;
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
        GDArb.velocity = new Vector3(0, 1f, 0);
    }
    void Update()
    {

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
    private void gravityDistortion(Vector3 center, float radius)
    {

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var other in hitColliders)
        {
            if (other.attachedRigidbody)
            {
                if(other.gameObject == player.gameObject || other.gameObject == player.head.gameObject)
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
        gravityInversion(directionOfGDA,numberOfGDA);
    }

    private void playerExit()
    {

        if (player.isInGDA != numberOfGDA)
            return;

        gravityInversion(1, 0);
    }

    private void gravityInversion(int newDirection,int newNumber)
    {
        int previousDirection = player.GDADirection;
        player.GDADirection = newDirection;
        player.isInGDA = newNumber;

        player.gorizontalAngle = 0;

        /*body.localEulerAngles = new Vector3(body.transform.rotation.x, rotationGorizontal, body.transform.rotation.z);*/

        player.inRotating = true;
        player.transform.localEulerAngles = new Vector3(rotationByX[newDirection], rotationByY[newDirection], rotationByZ[newDirection]);
        /*player.mainRigidbody.freezeRotation = false;
        player.transform.Rotate(0, 90, -90);
        player.mainRigidbody.freezeRotation = true;*/
        /*player.body.transform.rotation.x = rotationByX[newDirection];*/

        /*Debug.Log(rotationByX[newDirection]);
        Debug.Log(rotationByY[newDirection]);
        Debug.Log(rotationByZ[newDirection]);*/

        if (newNumber != 0)
            player.GetComponent<Rigidbody>().useGravity = false;
        else
            player.GetComponent<Rigidbody>().useGravity = true;
    }
}
