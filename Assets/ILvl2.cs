using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ILvl2 : MonoBehaviour
{
    public DialogueTriggerS2 trigger;
    public DialogueManagerS1 ds1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ds1 = GameObject.Find("Canvas").GetComponent<DialogueManagerS1>();
        trigger.TriggerDialogue();
    }
    public void Update()
    {
        if (ds1 != null && ds1.complete == true)
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}
