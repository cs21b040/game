using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueTriggerS2 trigger;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if collides with player tag
        if (collision.gameObject.tag == "Player")
        {
            trigger.TriggerDialogue();
        }

        if(collision.gameObject.name == "Car")
        {
            trigger.TriggerDialogue();
        }
    }
}
