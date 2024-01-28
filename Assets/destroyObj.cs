using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    public string temp;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene(temp);
        }
    }
}
