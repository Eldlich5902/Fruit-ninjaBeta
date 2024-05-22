using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOverMenu : MonoBehaviour
{
    //[SerializeField] GameObject gameOverMenu;
    public void Setting()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
            PhotonNetwork.LeaveRoom(true);
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            PhotonNetwork.SendAllOutgoingCommands();

        SceneManager.LoadScene("Menu");
          
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}
