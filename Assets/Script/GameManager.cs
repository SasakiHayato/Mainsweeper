using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlay = true;

    public void OnClickStart()
    {
        isPlay = true;
        SceneManager.LoadScene("Main");
    }

    private float time = 0;
    private void Update()
    {
        if (isPlay != false) return;

        time += Time.deltaTime;
        if (time > 3)
        {
            SceneManager.LoadScene("Rezult Scene");
        }
    }
}
