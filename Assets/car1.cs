using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car1 : MonoBehaviour
{
    public Rigidbody2D carRigidbody;
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public float speed = 20;
    public float carTorque = 10;
    public float maxSlopeAngle = 10;
    private float movement;
    //public int booster = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }


    public void FixedUpdate()
    {
        float slopeAngle = Vector2.Angle(Vector2.up, carRigidbody.transform.up);
        Debug.Log("Slope Angle: " + slopeAngle);
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




    }
}
