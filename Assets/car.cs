using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public Rigidbody2D carRigidbody;
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public float speed = 20;
    public float carTorque = 10;
    public float maxSlopeAngle = 10;
    public float brakeForce = 5;
    private bool isBraking = false;
    private float movement;
    public float maxVal = 1;
    public float curVal = 1;
    //public int booster = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(KeyCode.Space);
    }


    public void FixedUpdate()
    {
        float slopeAngle = Vector2.Angle(Vector2.up, carRigidbody.transform.up);
        // Check if the slope angle exceeds the maximum allowed angle
        if (slopeAngle <= maxSlopeAngle)
        {
            backTire.AddTorque(-movement * speed * Time.fixedDeltaTime);
            frontTire.AddTorque(-movement * speed * Time.fixedDeltaTime);
            carRigidbody.AddTorque(-movement * carTorque * Time.fixedDeltaTime);
        }
        else
        {
            // Adjust the car's rotation to prevent climbing the slope
            float correctionTorque = movement * carTorque * Time.fixedDeltaTime;
            carRigidbody.AddTorque(correctionTorque);
        }

        if (isBraking)
        {
            ApplyBrakes();
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
 