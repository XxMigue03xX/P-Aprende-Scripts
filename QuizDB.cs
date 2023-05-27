using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDB : MonoBehaviour
{
    [SerializeField] private List<Question> v_questionList = null;
    private List<Question> v_backup = null;

    private void Awake()
    {
        v_backup = v_questionList;
    }
    public Question GetRandom(bool remove = true)
    {
        if (v_questionList.Count == 0)
        {
            RestoreBackup();
        }
        int index = Random.Range(0, v_questionList.Count);
        if (!remove)
        {
            return v_questionList[index];
        }
        Question q = v_questionList[index];
        v_questionList.RemoveAt(index);
        return q;
    }
    private void RestoreBackup()
    {
        v_questionList = v_backup;
    }
}