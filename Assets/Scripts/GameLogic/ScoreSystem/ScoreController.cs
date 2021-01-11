using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    private static ScoreController instance;
    public static ScoreController Instance { get { if (instance == null) { instance = FindObjectOfType<ScoreController>(); } return instance;  } }

    public delegate void ScoreChangedEventHandler(int new_quantity, int delta);
    public event ScoreChangedEventHandler ScoreChangedEvent;

    public int CurrentScore { get => currentScore; set => currentScore = value; }
    [SerializeField]
    private int currentScore;

    public int CurrentKillScoreValue = 1;

    public void AddScore(int multiplier)
    {
        CurrentScore += CurrentKillScoreValue * multiplier;
        ScoreChangedEvent.Invoke(CurrentScore, CurrentKillScoreValue * multiplier);
    }

    public void SubscribeScoreObject(ScoreValue scoreable_obj)
    {
        scoreable_obj.ScoreChangedEvent += AddScore;
    }

}
