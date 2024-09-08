using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class gameOverScreen : MonoBehaviour
{

//code from learnt from  Coco code https://www.youtube.com/watch?v=K4uOjb5p3Io&t=222s
    public Text scoreboardText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreboardText.text = "Your Final Score: " + score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Restarting Game");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Returning to Main Menu");
    }
}