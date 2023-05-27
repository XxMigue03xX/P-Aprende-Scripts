using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public GameObject EnemyGenerator;
    public GameObject game;
    public AudioClip JumpClip;
    public AudioClip DieClip;
    public AudioClip PointClip;
    public ParticleSystem Dust;
    private Animator animator;
    private AudioSource AudioPlayer;
    private float StartY;
    // Start is called before the first frame update
    void Start()
    {
        StartY = transform.position.y;
        animator = GetComponent<Animator>();
        AudioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Variables booleanas
        bool IsGrounded = transform.position.y == StartY;
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;
        bool UserAction = (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0));
        //Iniciar juego y música
        if (IsGrounded && gamePlaying && UserAction)
        {
            UpdateState("PlayerJump");
            AudioPlayer.clip = JumpClip;
            AudioPlayer.Play();
        }
    }
    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Lógica del juego
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            EnemyGenerator.SendMessage("CancelGenerator", true);
            game.SendMessage("ResetTimeScale", 0.5f);
            DustStop();
            //Audio
            game.GetComponent<AudioSource>().Stop();
            AudioPlayer.clip = DieClip;
            AudioPlayer.Play();
        }
        else if (other.gameObject.tag == "Point")
        {
            game.SendMessage("IncreasePoints");
            AudioPlayer.clip = PointClip;
            AudioPlayer.Play();
        }
    }
    void DustPlay()
    {
        Dust.Play();
    }
    void DustStop()
    {
        Dust.Stop();
    }
}
