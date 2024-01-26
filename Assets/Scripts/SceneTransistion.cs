using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransistion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string transitionTo;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector2 exitDirection;
    [SerializeField] private float exitTime;
    public void Start()
    {
        if(GameManager.Instance.transitionedFrom == transitionTo)
        {
            PlayerController.Instance.transform.position = startPoint.position; 
            StartCoroutine(PlayerController.Instance.WalkIntoScene(exitDirection, exitTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("i");
        if (collision.CompareTag("Player"))
        {
            
            GameManager.Instance.transitionedFrom = SceneManager.GetActiveScene().name;
            PlayerController.Instance.pState.cutScene = true;
            SceneManager.LoadScene(transitionTo);
        }
    }
}
