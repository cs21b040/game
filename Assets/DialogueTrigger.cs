using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(messages, actors);
    }

    public void Start()
    {
        TriggerDialogue();
    }
}

[System.Serializable]
public class Message
{
    public int actorid;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public int actorid;
}