using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
       SceneManager.LoadScene(sceneName);
    }
}
