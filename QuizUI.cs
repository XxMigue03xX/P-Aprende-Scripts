using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Text Question = null;
    [SerializeField] private List<OptionButton> ButtonList = null;

    public void Construte(Question q, Action<OptionButton> CallBack)
    {
        Question.text = q.text;
        for (int n = 0; n < ButtonList.Count; n++)
        {
            ButtonList[n].Construte(q.Options[n], CallBack);
        }
    }
}
