using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text messageText;
    [SerializeField] private string sceneName="D-Lvl";
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerController.Instance.currLvl < 4)
            {
                messageText.text = "Complete all levels";
                StartCoroutine(HideTextAfterSeconds(5));
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
    IEnumerator HideTextAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageText.text = "";
    }
}
