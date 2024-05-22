using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReturnMainMenu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
