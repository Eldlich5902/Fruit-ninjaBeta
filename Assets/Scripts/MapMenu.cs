using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
    /*số màn chơi sau đc unlock sau khi hoàn thành màn chơi trc
    public Button[] buttons;
    private void Awake()
    {
        //PlayerPrefs.SetInt("UnlockedMap", 8);
        int unlockedMap = PlayerPrefs.GetInt("UnlockedMap",1);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedMap; i++)
        {
            buttons[i].interactable = true;
        }
    }*/

    //Load map
    public void OpenMap(int mapID)
    {
        string mapName = "Map " + mapID;
        SceneManager.LoadScene(mapName);
    }
}
