using UnityEngine;
using TMPro;

public class PlayerCollect : MonoBehaviour
{
    public delegate void CollectFruits(int fruit); //dinh nghia ham delegate
    public static CollectFruits collectFruitsDelegate; //khai bao

    private int numberFruits = 0;
    public int NumberFruits { get => this.numberFruits; set => this.numberFruits = value; }

    private void Start()
    {
        if (GameManager.HasInstance)
        {
            numberFruits = GameManager.Instance.Fruits;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            this.Collect(collision);
        }
    }

    private void Collect(Collider2D collision)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
        }

        this.numberFruits++;
        collision.gameObject.GetComponent<Animator>().SetTrigger("Collected");

        if (GameManager.HasInstance)
        {
            GameManager.Instance.UpdateFruits(numberFruits);
            collectFruitsDelegate(numberFruits); //Broadcast event
        }

    }
}
