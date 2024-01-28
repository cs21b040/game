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
    bool isColliding = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Obstacle")
        {
            Debug.Log("Collided");
            //velocity  is 0 for car and tires
            carRigidbody.velocity = Vector2.zero;
            backTire.velocity = Vector2.zero;
            frontTire.velocity = Vector2.zero;
            //angular velocity is 0 for car and tires
            carRigidbody.angularVelocity = 0;
            backTire.angularVelocity = 0;
            frontTire.angularVelocity = 0;
            isColliding = true;
        }
    }

    //public int booster = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(isColliding == true)
        {
            
            return;
        }
        movement = Input.GetAxis("Horizontal");
    }


    public void FixedUpdate()
    {
        float slopeAngle = Vector2.Angle(Vector2.up, carRigidbody.transform.up);
            backTire.AddTorque(-movement * speed * Time.fixedDeltaTime);
            frontTire.AddTorque(-movement * speed * Time.fixedDeltaTime);
            carRigidbody.AddTorque(-movement * carTorque * Time.fixedDeltaTime);

    }
}
