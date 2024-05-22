using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom(true);
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            PhotonNetwork.SendAllOutgoingCommands();
        }

    }
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
