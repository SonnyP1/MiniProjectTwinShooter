using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int _score;
    [SerializeField] UIScoreKeeper _uiScoreKeeper;

    void Start()
    {
        if (_uiScoreKeeper != null)
        {
            _uiScoreKeeper.UpdateScoreUI();
        }
        else
        {
            Debug.Log("UI score keeper not found!");
        }

    }
    public void AddOrSubScore(int AddOrSub)
    {
        _score += AddOrSub;
        _uiScoreKeeper.UpdateScoreUI();
        if (_score == 0)
        {
            _uiScoreKeeper.UpdateWinScreen();
        }
    }

    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int newScore)
    {
        _score = newScore;
    }
}
