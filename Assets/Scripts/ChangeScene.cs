using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
       public void moveToScene(int sceneId)
    {
        //TODO:: Dont forget to add Scene No in build settings
        SceneManager.LoadScene(sceneId);

    }
}
