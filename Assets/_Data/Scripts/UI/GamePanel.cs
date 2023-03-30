using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfFruits;
    [SerializeField] private TextMeshProUGUI ammunition;

    [SerializeField] private TextMeshProUGUI timeText;

    public TextMeshProUGUI NumberOfFruits => this.numberOfFruits;
    private float timeRemaining;
    private bool isTimerRunning = false;

    private void Awake()
    {
        if (GameManager.HasInstance)
        {
            this.SetTimeRemain(GameManager.Instance.TimeLevel1);
        }
    }

    private void OnEnable() //or use Start()
    {
        if (GameManager.HasInstance)
        {
            this.SetTimeRemain(GameManager.Instance.TimeLevel1);
        }
        this.isTimerRunning = true;
        PlayerCollect.collectFruitsDelegate += OnPlayerCollect;
    }

    private void OnDisable() //or use OnDestroy()
    {
        PlayerCollect.collectFruitsDelegate -= OnPlayerCollect;
    }

    private void OnPlayerCollect(int value)
    {
        this.numberOfFruits.SetText(value.ToString());
    }

    private void Update()
    {
        if (this.isTimerRunning)
        {
            if (this.timeRemaining > 0)
            {
                this.timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                this.timeRemaining = 0;
                this.isTimerRunning = false;
                if (UIManager.HasInstance && GameManager.HasInstance && AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_LOSE);
                    GameManager.Instance.PauseGame();
                    UIManager.Instance.ActiveLosePanel(true);
                }
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        this.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimeRemain(float seconds)
    {
        this.timeRemaining = seconds;
    }

    public void OnClickedPauseButton()
    {
        if (GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.PauseGame();
            UIManager.Instance.ActivePausePanel(true);
        }
    }
}
