using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class OnlineGameManager : MonoBehaviour
{
    public static OnlineGameManager Instance { get; private set; }

    [SerializeField] private GameObject bladeOnline;
    [SerializeField] private SpawnerOnline spawnerOnline;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreText2;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject goOver;

    public Leaderboard leaderboard;
    public PlayerManager playerManager;
    private int score;
    private int score2;
    public int Score => score;
    public int Score2 => score2;
    public AudioSource chemTrungHoaQua;//voice chém trúng hoa quả
    public AudioSource chemTrungBom;//voice chém trúng bom

    public PhotonView photonView;

    [SerializeField] private GameObject goSettingMenu;
    private bool isPaused = false;

    //PhotonView view;

    private void Awake()
    {
        bladeOnline = PhotonNetwork.Instantiate("BladeOnline", new Vector3(-20, 10, 0), Quaternion.identity);
        //Debug.Log("IsMasterClient " + PhotonNetwork.IsMasterClient);

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameOver();
    }
    /*Lần chơi mới (GameOver)*/
    private void GameOver()
    {
        Time.timeScale = 1f;

        ClearScene();

        //if (view.IsMine)
        {
            bladeOnline.GetComponent<BladeOnline>().enabled = true;
        }
        spawnerOnline.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
        score2 = 0;
        scoreText2.text = score2.ToString();
    }
    
    /*Đặt lại màn*/
    private void ClearScene()
    {
        OnlineFruit[] onlineFruits = FindObjectsOfType<OnlineFruit>();

        foreach (OnlineFruit onlineFruit in onlineFruits) {
            Destroy(onlineFruit.gameObject);
        }

        OnlineBomb[] onlineBombs = FindObjectsOfType<OnlineBomb>();

        foreach (OnlineBomb onlineBomb in onlineBombs) {
            Destroy(onlineBomb.gameObject);
        }
    }
    /*Điểm chém trúng hoa quả*/
    public void IncreaseScore(int points)
    {
      
        
            score += points;
            photonView.RPC("SyncScore", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, score);
        
       

        chemTrungHoaQua.Play();//bật voice chém trúng hoa quả

    }
    
    [PunRPC]
    void SyncScore(int playerNumber, int Score)
    {
        if (playerNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            score = Score;
            scoreText.text = "Mine:" + score;
        }
        else
        {
            score2 = Score;
            scoreText2.text = "Other:" + score2;
        }
    }

    /*Chém trúng bom*/
    public void OnlineExplode(int dmduanaochemtrungbom)
    {
        Debug.Log("Chém trúng bom");
        photonView.RPC("SyncBom", RpcTarget.All, dmduanaochemtrungbom);
    }
    [PunRPC]
    void SyncBom(int dmduanaochemtrungbom)
    {
        if (dmduanaochemtrungbom == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            scoreText.text = "MINE LOSE";
            scoreText2.text = "OTHER WIN";
        }
        else if (dmduanaochemtrungbom != -1)
        {
            scoreText.text = "MINE WIN";
            scoreText2.text = "OTHER LOSE";
        }
        else
        {
            if (score > score2)
            {
                scoreText.text = "P1 WIN";
                scoreText2.text = "P2 LOSE";
            }
            else if (score < score2)
            {
                scoreText.text = "P1 LOSE";
                scoreText2.text = "P2 WIN";
            }
        }
        bladeOnline.GetComponent<BladeOnline>().enabled = false;
        spawnerOnline.enabled = false;
        ClearScene();
        StartCoroutine(OnlineExplodeSequence());
        chemTrungBom.Play();//bật voice chém trúng bom
    }

    private IEnumerator OnlineExplodeSequence()/*hiệu ứng mờ dần*/
    {
        float elapsed = 0f;//Time hoạt ảnh duy trì
        float duration = 0.5f;//Time tồn tại hoạt ảnh tối đa

        // Mờ dần thành màu trắng
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        //yield return leaderboard.SubmitScoreRountine(score);
        //playerManager.UpdateHighScore();
        //GameOver();
        goOver.SetActive(true);
        /*MenuThongBao.Instance.Complete.SetActive(true);*/
        //ClearScene();

        elapsed = 0f;

        //spawner.enabled = false;

        // Mờ dần trở lại
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    /*Pause game online*/
    public void PauseGame()
    {
        photonView.RPC("SyncPauseStatus", RpcTarget.All, true);
        goSettingMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    [PunRPC]
    void SyncPauseStatus(bool paused)
    {
        
        isPaused = paused;
        Debug.Log("SyncPauseStatus " + isPaused.ToString());
        if (isPaused)
        {
            goSettingMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            goSettingMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
