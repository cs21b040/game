using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCarType : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    public float value;
    public void changeType()
    {
        car.GetComponent<car>().curVal = value ;
    }
}
