using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    [SerializeField] private float Speed = 5;
    int currentDir;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDir=PlayerController.Instance.transform.localScale.x < 0 ? 1 : -1;
        rb.velocity = new Vector2(currentDir * Speed, rb.velocity.y);
    }
    private void Update()
    {
        print(rb.velocity.x);
    }
}
