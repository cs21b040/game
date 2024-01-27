using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate
    public int len = 16;
    public float time = 2f;
    void Start()
    {
        
            StartCoroutine(temp());
        
    }
    IEnumerator temp()
    {
        yield return new WaitForSeconds(time);
        int temp = len / 2;
        Vector3 t = transform.position;
        t.x += Random.Range(-temp, temp);
        t.y += 5;
        Instantiate(prefab, t, Quaternion.identity);
    }
}
