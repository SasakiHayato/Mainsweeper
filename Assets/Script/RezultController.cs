using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RezultController : MonoBehaviour
{
    [SerializeField] private UiController ui;
    [SerializeField] private Text scoreText;

    private void Update()
    {
        scoreText.text = ui.score.ToString("Score:" + "0000");
    }
}
