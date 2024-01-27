using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carL : MonoBehaviour
{
    public Rigidbody2D carRigidbody;
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public float speed = 20;
    public float carTorque = 20;
    public float maxSlopeAngle = 30;
    private float movement;
    //public int booster = 1;
    // Start is called before the first frame update
    void Start()
    {
        //increase the friction of the tires

    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        
        if(frontTire.angularVelocity > 2500)
        {
            frontTire.angularVelocity = 2500;
        }
        
        if(backTire.angularVelocity > 2500)
        {
            backTire.angularVelocity = 2500;
        }
        

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
