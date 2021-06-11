using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlay = true;
    public bool clearCheck = true;

    [SerializeField] private UiController ui;

    public void OnClickStart()
    {
        isPlay = true;
        SceneManager.LoadScene("Main");
    }

    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private float time = 0;
    private void Update()
    {
        if (isPlay == true)
        {
            ui.TimeCheck();
            ui.ScoreCheck();
            ui.MyHpCheck();
        }
        else
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                ui.rezultScene.gameObject.SetActive(true);
            }
        }
    }

    public void ClearCheck()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
