using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManagerS1 : MonoBehaviour
{
    public Text messageText;
    public Text actorName;
    public Button continueButton;
    public RectTransform backgroundBox;
    Scene currentScene;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public bool complete;
    public void StartDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        complete = false;
        Debug.Log("Starting dialogue with " + currentMessages.Length + " messages.");
        DisplayMessage();
        backgroundBox.LeanScale(new Vector3(5.373f, 3.300f, 1.30f), 1.304f).setEaseInOutExpo();

    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

       
        if(messageText.text.EndsWith("let's move forward..."))
        {
            complete = true;
        }
        Debug.Log(messageText.text);
        if(messageText.text.EndsWith("Alex may help..!"))
        {
            complete = true;
        }
        if(messageText.text.EndsWith("start playing.."))
        {
            complete = true;
        }
        if(messageText.text.EndsWith("Open the parachute only when in air.."))
        {
            complete = true;
        }
        if (messageText.text.EndsWith(" to implement interfaces it does not use. Instead of one large  interface, it's better to have smaller, specific interfaces."))
        {
            complete = true;
        }

        Actor actorToDisplay = currentActors[messageToDisplay.actorid];
        actorName.text = actorToDisplay.name;
        AnimateTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage >= currentMessages.Length)
        {
            Debug.Log("No more messages.");
            backgroundBox.LeanScale(Vector3.zero, 1.304f).setEaseInOutExpo();
            if(currentScene.name == "O lvl 1.5")
            {
                SceneManager.LoadScene("O-Lvl 2");
            }
            if(currentScene.name == "O-Lvl 2")
            {
                SceneManager.LoadScene("O Lvl 2.5");
            }
            if(currentScene.name == "O Lvl 2.5")
            {
                SceneManager.LoadScene("O-Lvl 3");
            }
            if(currentScene.name == "O-Lvl 3")
            {
                SceneManager.LoadScene("S-Lvl2");
            }
            if(currentScene.name == "S-Lvl2")
            {
                SceneManager.LoadScene("LevelScene");
            }
            if(currentScene.name == "L-Lvl 1")
            {
                SceneManager.LoadScene("L Lvl Mech");
            }
            if(currentScene.name == "L Lvl Mech")
            {
                SceneManager.LoadScene("L-Lvl 2");
            }
            if(currentScene.name == "L-Lvl 2")
            {
                SceneManager.LoadScene("LevelScene");
            }

            return;
        }
        DisplayMessage();
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(actorName.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f).setEaseInOutExpo();
        LeanTween.textAlpha(actorName.rectTransform, 1, 0.5f).setEaseInOutExpo();
    }


    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
