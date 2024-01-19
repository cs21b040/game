using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexIntro : MonoBehaviour

   {

    GameObject alex;
    // Start is called before the first frame update
    void Start()
    {
        //set gravityscale of Alex to 0
        alex.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        alex = GameObject.Find("Alex");
    }

    public void Appear ()
    {
        alex.GetComponent<Rigidbody2D>().gravityScale = 3;
    }
}
