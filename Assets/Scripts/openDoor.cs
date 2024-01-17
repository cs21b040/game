using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int key;
    void Update()
    {
        if (PlayerController.Instance.currLvl >= key)
        {
            Destroy(gameObject);
        }
    }
}
        