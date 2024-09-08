using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
//code learnt from Coco Code https://www.youtube.com/watch?v=K4uOjb5p3Io&t=222s
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game exiting");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Game starting");
    }
}
