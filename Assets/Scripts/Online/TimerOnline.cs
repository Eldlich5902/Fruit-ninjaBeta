using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class TimerOnline : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    float times;
    public void TimeOutSetting()
    {
        Time.timeScale = 0;
        OnlineGameManager.Instance.OnlineExplode(-1);
    }

    void Start()
    {

        ExitGames.Client.Photon.Hashtable setValue = new ExitGames.Client.Photon.Hashtable();
        setValue.Add("val", remainingTime);
        PhotonNetwork.CurrentRoom.SetCustomProperties(setValue);
        
    }
    void Update()
    {
        times = Convert.ToSingle(PhotonNetwork.CurrentRoom.CustomProperties["val"]);
        //Debug.Log("times " + times);
        if(times > 0 )
        {
            times -= Time.deltaTime;
            PhotonNetwork.CurrentRoom.CustomProperties["val"] = times;
        }
        //thoi gian dem nguoc ve 0 thi dung
        else if (times < 0 )
        {
            times = 0;
            PhotonNetwork.CurrentRoom.CustomProperties["val"] = times;
            //Time Out
            timerText.color = Color.red;
            TimeOutSetting();
        }
        ////chia theo phut, giay
        int minutes = Mathf.FloorToInt(times / 60);
        int seconds = Mathf.FloorToInt(times % 60);
        //dinh dang bo dem
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
