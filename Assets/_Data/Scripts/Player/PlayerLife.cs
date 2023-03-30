using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerCollect playerCollect;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform checkPoint;

    private int numberFruitsCP;
    private bool isDead;

    private void Awake()
    {
        this.playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            this.Die();
        }
    }

    private void Die()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_DEATH);
        }

        this.isDead = true;
        this.rb.bodyType = RigidbodyType2D.Static;
        this.animator.SetTrigger("Death");

    }

    public void Restart()
    {
        this.transform.position = this.playerSpawnPoint.position;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        animator.Rebind();
        this.isDead = false;
    }

    public bool IsDead() => this.isDead;

    //public void SaveCheckPoint(Transform checkPoint)
    //{
    //    this.checkPoint = checkPoint;
    //    this.numberFruitsCP = this.playerCollect.NumberFruits;
    //}

    //private void LoadCheckPoint()
    //{
    //    transform.position = this.checkPoint.position;
    //    this.playerCollect.NumberFruits = this.numberFruitsCP;
    //    this.playerCollect.SetText();
    //}
}
