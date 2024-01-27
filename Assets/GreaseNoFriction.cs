using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEngine;

public class GreaseNoFriction : MonoBehaviour
{
    public Rigidbody2D carRigidbody;
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public DialogueTriggerS2 trigger;

    bool isColliding = false;
    int count = 0;

    // Physics material with zero friction
    private PhysicsMaterial2D noFrictionMaterial;

    void Start()
    {
        // Create a physics material with zero friction
        noFrictionMaterial = new PhysicsMaterial2D();
        noFrictionMaterial.friction = 0;
    }

    // Apply the physics material to the tire collider
    private void ApplyFrictionToTire(Rigidbody2D tire)
    {
        if (tire != null)
        {
            Collider2D tireCollider = tire.GetComponent<Collider2D>();
            if (tireCollider != null)
            {
                tireCollider.sharedMaterial = noFrictionMaterial;
            }
        }
    }

    // OnCollisionEnter2D is called when the car collides with an object
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "tire")
        {
            isColliding = true;
            ApplyFrictionToTire(frontTire);
            ApplyFrictionToTire(backTire);
            

        }
    }

    void Update()
    {
        Debug.Log(frontTire.angularVelocity);
        if(isColliding == true)
        {
            carRigidbody.velocity = Vector2.zero;
            if (frontTire.angularVelocity > 2000 || frontTire.angularVelocity < -2000)
            {
                frontTire.angularVelocity = 0;
                count++;
            }
            if (backTire.angularVelocity >2000 || backTire.angularVelocity < -2000)
            {
                backTire.angularVelocity = 0;
            }
        }
        if (count == 2)
        {
            //trigger dialogue
            trigger.TriggerDialogue();
            count = 4;
        }

        
    }
}

