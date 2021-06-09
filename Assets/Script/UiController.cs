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

    [SerializeField] public GameObject rezultScene;

    public float timer = 9999;
    public int score = 0;

    private void Start()
    {
        rezultScene.gameObject.SetActive(false);
    }

    public void TimeCheck()
    {
        timer -= Time.deltaTime;
        timeText.text = timer.ToString("Time:" + "00");

        if (timer <= 0)
        {
            manager.isPlay = false;
        }
    }

    public void ScoreCheck()
    {
        score = main.maxCount * 1000;
        
        scoreText.text = score.ToString("Score:" + "0000");
    }
}
