using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;
   public void Setting()
   {
        settingMenu.SetActive(true);
        Time.timeScale = 0;
   }
   public void Home()
   {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
   }
   public void Resume()
   {
        settingMenu.SetActive(false);
        Time.timeScale = 1;
   }
   public void Restart()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
   }
}
