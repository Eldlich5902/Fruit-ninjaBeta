using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Bắt đầu
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    //Thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}
