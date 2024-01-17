using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    PlayerController player;
    public GameObject[] heartContainers;
    public Image[] heartFills;
    public Transform heartParent;
    public GameObject heartContainerPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        heartContainers = new GameObject[PlayerController.Instance.maxHealth];
        heartFills = new Image[PlayerController.Instance.maxHealth];
        PlayerController.Instance.OnHealthChangeCallback += UpdateHearts;
        CreateHeartContainers();
        UpdateHearts();
    }

    void Update()   
    {

    }
    void SetHeartContainers()
    {
        for(int i = 0; i < heartContainers.Length; i++)
        {
                heartContainers[i].SetActive(true);
            if(i<PlayerController.Instance.health)
            {
            }
            else
            {
                //heartContainers[i].SetActive(false);
            }
        }
    }
    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < PlayerController.Instance.health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }
    }
    void CreateHeartContainers()
    {
        for(int i = 0; i < PlayerController.Instance.maxHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartParent,false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("Heart_Fill").GetComponent<Image>();

        }
    }
    void UpdateHearts()
    {
        SetHeartContainers();
        SetFilledHearts();
    }
}