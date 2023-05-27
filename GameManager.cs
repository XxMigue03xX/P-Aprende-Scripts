using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip v_correctSound = null;
    [SerializeField] private AudioClip v_incorrectSound = null;
    [SerializeField] private Color v_correctColor = Color.black;
    [SerializeField] private Color v_incorrectColor = Color.black;
    [SerializeField] private float v_waitTime = 0.0f;
    
    private QuizDB v_quizDB = null;
    private QuizUI v_quizUI = null;
    private AudioSource v_audioSource = null;
    private void Start()
    {
        v_quizDB = GameObject.FindObjectOfType<QuizDB>();
        v_quizUI = GameObject.FindObjectOfType<QuizUI>();
        v_audioSource = GetComponent<AudioSource>();
        NextQuestion();
    }

    private void NextQuestion()
    {
        v_quizUI.Construte(v_quizDB.GetRandom(),GiveAnswer);
    }
    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutime(optionButton));
    }
    private IEnumerator GiveAnswerRoutime(OptionButton optionButton)
    {
        if (v_audioSource.isPlaying)
        {
            v_audioSource.Stop();
        }

        v_audioSource.clip = optionButton.Option.v_correct ? v_correctSound : v_incorrectSound;
        optionButton.SetColor(optionButton.Option.v_correct ? v_correctColor : v_incorrectColor);
        v_audioSource.Play();
        yield return new WaitForSeconds(v_waitTime);
        NextQuestion();
    }
}