using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public void TimeOutSetting()
    {
        Time.timeScale = 0;
        GameManager.Instance.Explode();
    }
    void Update()
    {
        //thoi gian dem nguoc ve 0 thi dung
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //Time Out
            timerText.color = Color.red;
            TimeOutSetting();
        }
        //chia theo phut, giay
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        //dinh dang bo dem
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
