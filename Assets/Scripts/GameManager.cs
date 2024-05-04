using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject goOver;

    public Leaderboard leaderboard;
    public PlayerManager playerManager;
    private int score;
    public int Score => score;
    public AudioSource chemTrungHoaQua;//voice chém trúng hoa quả
    public AudioSource chemTrungBom;//voice chém trúng bom

    private void Awake()
    {
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

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        /*MenuThongBao.Instance.Complete.SetActive(false);*/
    }
    
    
    /*Đặt lại màn*/
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }
    /*Điểm chém trúng hoa quả*/
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            PlayerPrefs.GetFloat("hiscore");
        }
        chemTrungHoaQua.Play();//bật voice chém trúng hoa quả
        /*if(score >= 10)
        {
            MenuThongBao.Instance.Complete.SetActive(true);
            ClearScene();
            spawner.enabled = false;

            Debug.Log("UnlockedMap=" + PlayerPrefs.GetInt("UnlockedMap"));
            if (PlayerPrefs.GetInt("UnlockedMap") < 8) UnlockNewMap();
        }*/
    }
    /*public void UnlockNewMap()
    {
        Debug.Log("buidIndex=" + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("RecheIndex=" + PlayerPrefs.GetInt("RecheIndex"));
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("RecheIndex"))
        {
                PlayerPrefs.SetInt("RecheIndex", SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("UnlockedMap", PlayerPrefs.GetInt("UnlockedMap", 1) + 1);
                PlayerPrefs.Save();
        }
    }*/

    /*Chém trúng bom*/
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        ClearScene();
        StartCoroutine(ExplodeSequence());
        chemTrungBom.Play();//bật voice chém trúng bom
    }

    private IEnumerator ExplodeSequence()/*hiệu ứng mờ dần*/
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
        yield return leaderboard.SubmitScoreRountine(score);
        playerManager.UpdateHighScore();
        //GameOver();
        this.goOver.SetActive(true);
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

}
