using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OLvl1Dialogue : MonoBehaviour
{
    int counter = 0;
    public DialogueTriggerS2 trigger;
    public DialogueManagerS1 ds1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ds1 = GameObject.Find("Canvas").GetComponent<DialogueManagerS1>();
        if(collision.gameObject.name == "tire")
        {
            counter++;
        }

        if(counter == 2)
        {
            counter = 3;
            trigger.TriggerDialogue();
            Debug.Log("hello" + ds1.complete);
        }
    }
    public void Update()
    {
        if (ds1!=null && ds1.complete == true)
        {
            SceneManager.LoadScene("O lvl 1.5");
        }
    }
}
