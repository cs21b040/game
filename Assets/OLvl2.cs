using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLvl2 : MonoBehaviour
{
    public DialogueTriggerS2 trigger;
    public GameObject SmokeSpirit;
    public Vector3 position;
    public Transform t;

    void OnCollisionEnter2D(Collision2D collision)
    {
        t=GameObject.Find("Car").transform;
        Fire();
        trigger.TriggerDialogue();
    }

    void Fire()
    { 
    Vector3 parachutePosition = new Vector3(t.position.x - 5, t.position.y-2, t.position.z);
    GameObject parachute = Instantiate(SmokeSpirit, parachutePosition, Quaternion.identity);
    parachute.transform.localScale = new Vector3(0.005f,0.005f,1);
    parachute.transform.parent = t;

    }
}
