using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuThongBao : MonoBehaviour
{
    public static MenuThongBao Instance { get; private set; }
    public GameObject Complete;
    private void Awake()
    {
        if (Instance != null)
        {
            //DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Retry()
    {
        
    }

    public void MenuReturn()
    {
        
        SceneManager.LoadScene("Menu");
        this.Complete.SetActive(false);
    }

    
}
