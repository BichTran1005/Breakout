using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int scores = 0;
    private int lives = 1;
    private int level = 1;
    private int time = 0;
    private int timeCountdown = 0;

    public LevelData LevelData = new LevelData();

    public void NewGame()
    {
        scores = 0;
        lives = 1;
        level = 1;
        LevelData = LevelDesign.Instance.LevelDatas[level - 1];

        UIManager.Instance.UpdateScoreText(scores);
        LoadLevel(LevelData);
        StartCoroutine(TimeCountdown());
        PlayerBar.Instance.ResetPlayerBarPosition();
    }

    private void LoadLevel(LevelData levelData)
    {
        GameController.Instance.CreateListBlock(levelData.ColAmount, levelData.BrickAmount);

        time = levelData.Time;
        timeCountdown = time;
        Ball.Instance.SetSpeed(levelData.SpeedBall);
        Ball.Instance.ReStartBall();
    }

    public void BallOut()
    {
        lives--;

        if(lives <= 0)
        {
            timeCountdown = 0;
            Ball.Instance.StopBall();
        }
        else
        {
            Ball.Instance.ResetBallPosition();
            Ball.Instance.ReStartBall();
        }
    }

    public void BallHitBrick(int score)
    {
        scores += score;
        UIManager.Instance.UpdateScoreText(scores);
        GameController.Instance.UpdateBlockCount();
    }

    private void EndGame()
    {
        UIManager.Instance.EndGame(Define.EndGame);

        ResetObject();
    }

    private void ResetObject()
    {
        Ball.Instance.ResetBallPosition();
        PlayerBar.Instance.ResetPlayerBarPosition();
        ObjectPool.Instance.ReturnAll();
    }

    public void NextLevel()
    {
        level++;

        if(level > LevelDesign.Instance.LevelDatas.Length)
        {
            level = 1;
        }

        LevelData = LevelDesign.Instance.LevelDatas[level - 1];
        ResetObject();
        LoadLevel(LevelData);
    }

    private IEnumerator TimeCountdown()
    {
        timeCountdown = time;
        UIManager.Instance.UpdateTimeText(timeCountdown);

        while (timeCountdown > 0)
        {
            timeCountdown--;
            UIManager.Instance.UpdateTimeText(timeCountdown);
            yield return new WaitForSeconds(1);
        }

        if(timeCountdown <= 0 )
        {
            EndGame();
        }    

        yield return null;
    }   
    
    public LevelData GetlevelData()
    {
        return LevelData;
    }
}
