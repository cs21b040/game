using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILvl2 : MonoBehaviour
{
    public DialogueTriggerS2 trigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trigger.TriggerDialogue();
    }
}
