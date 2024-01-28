using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoLevelScene : MonoBehaviour
{
    // Start is called before the first frame update
    private DialogueManagerS1 ds1;
    void Start()
    {
         ds1= this.GetComponent<DialogueManagerS1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ds1.complete == true)
        {
            print("hi");
            PlayerController.Instance.currLvl += 1;
            SceneManager.LoadScene("LevelScene");
        }
    }
}
