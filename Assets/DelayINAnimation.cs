using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayINAnimation : MonoBehaviour
{
    public float time;
    IEnumerator Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();
        spriteRenderer.enabled = false;
        animator.enabled = false;
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = true;
        animator.enabled = true;
    }
}