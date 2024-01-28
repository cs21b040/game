using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToLVL : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carObj;
    public car carScript;
    public float temp;
    void Start()
    {
        carScript = carObj.GetComponent<car>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (carScript.curVal !=temp ) 
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}
