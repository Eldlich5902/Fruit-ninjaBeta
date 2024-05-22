using Photon.Pun;
using UnityEngine;

public class OnlineBomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)/*kiểm tra va chạm của blade với vật thể*/
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;

            if (other.gameObject.GetComponent<PhotonView>() != null)
            {
                Debug.Log("My boom " + other.gameObject.GetComponent<PhotonView>().IsMine);

                if (other.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    OnlineGameManager.Instance.OnlineExplode(PhotonNetwork.LocalPlayer.ActorNumber);
                }
            }
        }
        // if (other.CompareTag("Player"))
        //{
        //    GetComponent<Collider>().enabled = false;
        //    OnlineGameManager.Instance.OnlineExplode(PhotonNetwork.LocalPlayer.ActorNumber);
        //}
        //if (other.CompareTag("Player"))
        //{
        //    GetComponent<Collider>().enabled = false;
        //    OnlineGameManager.Instance.OnlineExplode(1);
        //}


    }
}
