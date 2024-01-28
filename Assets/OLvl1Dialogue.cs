using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLvl1Dialogue : MonoBehaviour
{
    int counter = 0;
    public DialogueTriggerS2 trigger;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "tire")
        {
            counter++;
        }

        if(counter == 2)
        {
            counter = 3;
            trigger.TriggerDialogue();
        }
    }
}
