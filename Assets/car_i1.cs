using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_i1 : MonoBehaviour
{
    public Rigidbody2D carRigidbody;
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public float speed = 20;
    public float carTorque = 10;
    public float brakeForce = 5;
    private bool isBraking = false;
    private float movement;

    public GameObject para;

    private float parachuteOffsetX = -6f;
    private float parachuteOffsetY = 2f;
    private float parachuteOffsetZ = 0f;

    private bool parachuteDeployed = false;
    private GameObject deployedParachute;


    private bool isGrounded = false;

    void paraStart()
    {

        Vector3 carTopPoint = transform.position + new Vector3(parachuteOffsetX, parachuteOffsetY, parachuteOffsetZ);

     
        deployedParachute = Instantiate(para, carTopPoint, Quaternion.identity);
        deployedParachute.transform.localScale = new Vector3(5, 5, 1);
        deployedParachute.transform.parent = transform;

        parachuteDeployed = true;


        carRigidbody.freezeRotation = true;
    }


    void DismantleParachute()
    {
        if (deployedParachute != null)
        {
            Destroy(deployedParachute);
            deployedParachute = null;
            parachuteDeployed = false;

       
            carRigidbody.freezeRotation = false;
        }
    }



    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.P) && !parachuteDeployed)
        {
            paraStart();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !parachuteDeployed)
        {
            paraStart();
        }

        if (Input.GetKeyDown(KeyCode.E) && parachuteDeployed)
        {
            DismantleParachute();
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            DismantleParachute();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    public void FixedUpdate()
    {
        float tireRotationReductionFactor = 0.3f;

        float tireRotationTorque = -movement * speed * Time.fixedDeltaTime;

        backTire.AddTorque(tireRotationTorque);
        frontTire.AddTorque(tireRotationTorque);

        carRigidbody.AddTorque(-movement * carTorque * Time.fixedDeltaTime);
        if (isBraking)
        {
            ApplyBrakes();
        }
        if (parachuteDeployed)
        {
            float reductionFactor = 0.1f;

            carRigidbody.velocity = new Vector2(
                carRigidbody.velocity.x * (1 - reductionFactor * 1.2f),
                carRigidbody.velocity.y * (1 - reductionFactor * 1.2f) 
            );
            tireRotationTorque += tireRotationReductionFactor;
            backTire.angularVelocity *= Mathf.Clamp01(1 - tireRotationReductionFactor);
            frontTire.angularVelocity *= Mathf.Clamp01(1 - tireRotationReductionFactor);

            if (Mathf.Abs(backTire.angularVelocity) < 0.1f)
            {
                backTire.angularVelocity = 0f;
            }
            if (Mathf.Abs(frontTire.angularVelocity) < 0.1f)
            {
                frontTire.angularVelocity = 0f;
            }

            if (isGrounded)
            {
                DismantleParachute();
            }
        }
        else
        {
            carRigidbody.AddTorque(-movement * carTorque * Time.fixedDeltaTime);
        }
    }
    private void ApplyBrakes()
    {
        // Apply braking force to gradually stop the car
        carRigidbody.velocity = Vector2.Lerp(carRigidbody.velocity, Vector2.zero, brakeForce * Time.deltaTime);
        carRigidbody.angularVelocity = Mathf.Lerp(carRigidbody.angularVelocity, 0f, brakeForce * Time.deltaTime);

        // Stop the back and front tires
        backTire.angularVelocity = 0f;
        frontTire.angularVelocity = 0f;
    }

}
