using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private float timeLevel1;
    [SerializeField] private float timeLevel2;

    private int fruits = 0;
    private bool isPlaying;

    public int Fruits => this.fruits;
    public bool IsPlaying => this.isPlaying;
    public float TimeLevel1 => this.timeLevel1;
    public float TimeLevel2 => this.timeLevel2;

    public void UpdateFruits(int value)
    {
        this.fruits = value;
    }

    public void StartGame()
    {
        this.isPlaying = true;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        if (this.isPlaying)
        {
            this.isPlaying = false;
            Time.timeScale = 0.0f;
        }
    }

    public void ResumeGame()
    {
        this.isPlaying = true;
        Time.timeScale = 1.0f;
    }


    public void RestarGame()
    {
        this.fruits = 0;
        ChangeScene("Menu");

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActiveGamePanel(false);
            UIManager.Instance.ActiveSettingPanel(false);
            UIManager.Instance.ActivePausePanel(false);
            UIManager.Instance.ActiveLosePanel(false);
            UIManager.Instance.ActiveVictoryPanel(false);
            UIManager.Instance.GamePanel.NumberOfFruits.SetText("0");
        }
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
