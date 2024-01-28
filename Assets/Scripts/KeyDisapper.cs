using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDisapper : MonoBehaviour
{
    public GameObject Door;
    public GameObject Transit;
    public GameObject Light;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if collides with player tag
        if (collision.gameObject.tag == "Player")
        {
            //destroy key
            Destroy(gameObject);
            print("Key Disappeared");
            Door.SetActive(true);
            Transit.SetActive(true);
            Destroy(Light);
            //destroy sarah
            GameObject.Find("Sarah").SetActive(false);
        }
    }
    
}
