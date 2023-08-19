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
    private Action gameOverAction;
    public Action GameOverAction => gameOverAction;

    private readonly GameScene gameScene;

    private bool isGameOver;


    #endregion

    private void Start()
    {
        gameOverAction += SetGameOver;
    }

    #region GameOver
    private void SetGameOver()
    {
        isGameOver = true;
    }

    public bool CheckGameOver()
    {
        return isGameOver;
    }
    #endregion

    public GameScene CheckGameScene()
    {
        return gameScene;
    }
}
