using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameManager manager;
    [SerializeField] private Mainsweeper main;

    private float timer = 10;
    private int score = 0;
    void Update()
    {
        if (manager.isPlay != true) return;
        TimeCheck();
        ScoreCheck();
    }

    void TimeCheck()
    {
        timer -= Time.deltaTime;
        timeText.text = timer.ToString("0000");

        if (timer <= 0)
        {
            manager.isPlay = false;
        }
    }

    void ScoreCheck()
    {
        score = main.maxCount * 1000;
        
        scoreText.text = score.ToString("0000");
    }
}
