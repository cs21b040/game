using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLvl2 : MonoBehaviour
{
    public DialogueTriggerS2 trigger;

    void OnCollisionEnter2D(Collision2D collision)
    {
        trigger.TriggerDialogue();
    }
}
