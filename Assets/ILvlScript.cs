using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILvlScript : MonoBehaviour
{

    public DialogueTriggerS2 trigger;
    public GameObject SmokeSpirit;
    public Vector3 position;
    public Transform t;
    public Rigidbody2D car;
    bool destroy = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;
        t = GameObject.Find(name).transform;
        destroy = true;
        Fire();
        trigger.TriggerDialogue();
    }

    void Fire()
    {
        Vector3 parachutePosition = new Vector3(t.position.x, t.position.y+2, t.position.z+2);
        GameObject parachute = Instantiate(SmokeSpirit, parachutePosition, Quaternion.identity);
        parachute.transform.localScale = new Vector3(0.005f, 0.005f, 1);
        parachute.transform.parent = t;

    }

    private void Update()
    {
        if (destroy == true)
        {
            //velocity  is 0 for car
            car.velocity = Vector2.zero;
        }
    }
}
