using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreKeeper : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _youWinText;
    [SerializeField] int _amountOfItemToGrabToWin;
    private ScoreKeeper _scoreKeeper;

    void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (_amountOfItemToGrabToWin == null || _scoreText == null)
        {
            Debug.Log("UIScoreKeeper is missing a field");
        }
        else
        {
            if (_scoreKeeper == null)
            {
                Debug.Log("Scorekeeper was not found");
            }
            _scoreKeeper.SetScore(_amountOfItemToGrabToWin);
            _scoreText.text = _amountOfItemToGrabToWin.ToString();
        }
        
    }
    public void UpdateScoreUI()
    {
        _scoreText.text = _scoreKeeper.GetScore().ToString();
    }

    public void UpdateWinScreen()
    {
        _youWinText.enabled = true;
    }
}
