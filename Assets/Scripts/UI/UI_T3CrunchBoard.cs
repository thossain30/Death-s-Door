using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_T3CrunchBoard : MonoBehaviour
{
    private float endTime;


    private float swapTime;
    private float timeTillSwap;
    private Color baseColor;
    public Color redColor;

    public GameObject timerPanel;
    public GameObject scorePanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        SetPanelActive(false);
        baseColor = timerText.color;
    }

    void Update()
    {
        if (Time.time < endTime)
        {
            float min = (int)(endTime - Time.time) / 60;
            float sec = (int)(endTime - Time.time) % 60;
            float ms = ((endTime - Time.time) % 1) * 100;//) * 100) % 100;

            timerText.text = string.Format("{0:0}:{1:00};{2:00}", min, sec, ms);


            if (sec < 60) swapTime = 4;
            if (sec < 30) swapTime = 2;
            if (sec < 15) swapTime = 1;
            if (sec < 5) swapTime = 0.5f;
            if (sec < 2) swapTime = 0.25f;

            if (timeTillSwap <= 0)
            {
                SwapColor();
            }
            timeTillSwap -= Time.deltaTime;
        }
        else
        {
            timerText.text = "0:00;00";
            timerText.color = baseColor;
            scoreText.color = baseColor;
        }
    }

    private void SwapColor()
    {
        if (timerText.color == baseColor)
        {
            timerText.color = redColor;
            scoreText.color = redColor;
        }
        else
        {
            timerText.color = baseColor;
            scoreText.color = baseColor;
        }

        timeTillSwap = swapTime;
    }

    public void ResetValues()
    {
        scoreText.text = "0";
        timerText.text = "0:00;00";
        timerText.color = baseColor;
        scoreText.color = baseColor;
    }

    public void SetPanelActive(bool isActive)
    {
        timerPanel.SetActive(isActive);
        scorePanel.SetActive(isActive);
    }

    public void StartTimer(float endTime)
    {
        this.endTime = endTime;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
