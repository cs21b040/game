using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDisapper : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if collides with player tag
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //destroy sarah
            GameObject.Find("Sarah").SetActive(false);
        }
    }
    
}
