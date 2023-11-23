using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Text ScoreTxt;
    public Text TimeTxt;
    public Text GameStatusTxt;
    public GameObject UIStartGame;

    public void UpdateScoreText(int score)
    {
        ScoreTxt.text = $"{score}";
    }

    public void UpdateTimeText(int time)
    {
        TimeTxt.text = $"{time}";
    }

    public void StartGame()
    {
        UIStartGame.SetActive(false);
        GameManager.Instance.NewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateGameStatus(string gameStatus)
    {
        GameStatusTxt.text = gameStatus;
    }

    public void EndGame(string status)
    {
        UIStartGame.SetActive(true);
        UpdateGameStatus(status);
    }
}
