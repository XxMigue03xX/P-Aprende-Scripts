using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { Idle, Playing, Ended };
public class GameController : MonoBehaviour
{
    [Range(0f,0.2f)]
    public float ParallaxSpeed = 0.02f;
    public RawImage Background;
    public RawImage Platform;
    public GameState gameState = GameState.Idle;
    public GameObject Text;
    public GameObject Score;
    public GameObject player;
    public GameObject enemyGenerator;
    public Text PointsText;
    public float ScaleTime = 6f;
    public float ScaleInc = 0.25f;
    private AudioSource MusicPlayer;

    private int Points = 0;
    // Start is called before the first frame updat
    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        bool UserAction = (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0));
        //Empieza el juego
        if (gameState == GameState.Idle && UserAction)
        {
            gameState = GameState.Playing;
            Text.SetActive(false);
            Score.SetActive(true);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            player.SendMessage("DustPlay");
            MusicPlayer.Play();
            InvokeRepeating("GameTimeScale", ScaleTime, ScaleTime);
        }
        //Jugando
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }
        void Parallax()
        {
            float finalSpeed = ParallaxSpeed * Time.deltaTime;
            Background.uvRect = new Rect(Background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            Platform.uvRect = new Rect(Platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        }
    }
    public void RestartGame()
    {
        ResetTimeScale();
        SceneManager.LoadScene("Bonus");
    }
    void GameTimeScale()
    {
        Time.timeScale += ScaleInc;
        Debug.Log("Ritmo Incrementado: " + Time.timeScale.ToString());
    }
    public void ResetTimeScale(float NewTimeScale = 1f)
    {
     CancelInvoke("GameTimeScale");
     Time.timeScale = NewTimeScale;
     Debug.Log("Ritmo restablecido: " + Time.timeScale.ToString());
    }
    public void IncreasePoints()
    {
        PointsText.text = (++Points).ToString();
    }
    public void SaveScore(int CurrentPoints)
    {
        PlayerPrefs.SetInt("Max Points", CurrentPoints);
    }
}