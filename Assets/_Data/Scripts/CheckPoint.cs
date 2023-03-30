using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isCollideYet = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.isCollideYet)
        {
            this.SetCheckPoint(collision);
        }
    }

    private void SetCheckPoint(Collider2D collision)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_CHECKPOINT);
        }

        this.isCollideYet = true;
        this.animator.SetTrigger("Collided");

        //collision.GetComponent<PlayerLife>().SaveCheckPoint(transform);
    }
}
