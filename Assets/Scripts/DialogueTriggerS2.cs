using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerS2 : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerS1>().StartDialogue(messages, actors);
    }
}