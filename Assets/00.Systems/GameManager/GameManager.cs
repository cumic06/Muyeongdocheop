using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameScene
{
    Main,
    Stage,
    Loading
}

public class GameManager : MonoSingleton<GameManager>
{
    #region º¯¼ö
    private readonly GameScene gameScene;

    private bool isGameOver;

    private float time;
    #endregion

    private void Start()
    {
        AddTime();
        UpdateSystem.Instance.AddUpdateAction(AddTime);
    }

    #region GameOver
    public bool CheckGameOver()
    {
        return isGameOver;
    }

    public void GameOver()
    {

    }
    #endregion

    public GameScene CheckGameScene()
    {
        return gameScene;
    }

    #region Time
    private void AddTime()
    {
        time += Time.deltaTime;
    }

    public float GetTime()
    {
        return time;
    }
    #endregion
}