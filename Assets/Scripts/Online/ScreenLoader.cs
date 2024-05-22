using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScreenLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Loading");
    }
}
