using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool levelComplete = false;

    private void Awake()
    {
        this._animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.levelComplete)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
            }

            this._animator.Play("End_Pressed");
            this.levelComplete = true;
            //Invoke("CompleteLevel", 2f);
        }
    }

    public void CompleteLevel() //run at the end of End_Pressed animation
    {
        if (SceneManager.GetActiveScene().name.Equals("SceneLv2"))
        {
            if (UIManager.HasInstance && AudioManager.HasInstance)
            {
                Time.timeScale = 0f;
                UIManager.Instance.ActiveVictoryPanel(true);
                AudioManager.Instance.PlaySE(AUDIO.SE_VICTORY);

                return;
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (GameManager.HasInstance && UIManager.HasInstance && AudioManager.HasInstance)
        {
            UIManager.Instance.GamePanel.SetTimeRemain(GameManager.Instance.TimeLevel2);
            AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_04);
        }
    }
}
