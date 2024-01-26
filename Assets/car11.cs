using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car1 : MonoBehaviour
{
    public Rigidbody2D carRigidbody1;
    public Rigidbody2D backTire1;
    public Rigidbody2D frontTire1;
    public float speed1 = 20;
    public float carTorque1 = 10;
    private float movement1;
    //public int booster = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement1 = Input.GetAxis("Horizontal");
    }


    public void FixedUpdate()
    {
        backTire1.AddTorque(-movement1 * speed1 * Time.fixedDeltaTime);
        frontTire1.AddTorque(-movement1 * speed1 * Time.fixedDeltaTime);
        carRigidbody1.AddTorque(-movement1 * carTorque1 * Time.fixedDeltaTime);



    }
}
