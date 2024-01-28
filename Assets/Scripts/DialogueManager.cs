using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text messageText;
    public Text actorName;
    public Button continueButton;
    public RectTransform backgroundBox;
    Scene currentScene;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    public void StartDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        Debug.Log("Starting dialogue with " + currentMessages.Length + " messages.");
        DisplayMessage();
        backgroundBox.LeanScale(new Vector3(5.373f, 3.300f, 1.30f), 1.304f).setEaseInOutExpo();

    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorid];
        actorName.text = actorToDisplay.name;

        //if messageText starts with "Meet Alex", then call Appear() from AlexIntro.cs
        if (messageText.text.StartsWith("Meet Alex"))
        {
            GameObject alex = GameObject.Find("Alex");
            alex.GetComponent<Rigidbody2D>().gravityScale = 3;
        }

        if (messageText.text.EndsWith("start playing.."))
        {
            GameObject alex = GameObject.Find("Alex");
            Destroy(alex);
        }
        AnimateTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage >= currentMessages.Length)
        {
            Debug.Log("No more messages.");
            NextScene();
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
        //get element by name -> button -> onclick -> add dialogue manager -> next message
    }

    public void NextScene()
    {
        Debug.Log("Next scene");
        print(currentScene.name);

        string NextScene;
        if (currentScene.name == "Introduction")
        {
            NextScene = "Mechanic Scene";
        }
        else
        {
            NextScene = "LevelScene";
        }
        
        SceneManager.LoadScene(NextScene);

    }
}
