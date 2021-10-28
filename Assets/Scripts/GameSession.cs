using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;

    private void Awake()
    {
        SetUpSinglet();
    }

    private void SetUpSinglet()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return this.score;
    }

    public void AddToScore(int points)
    {
        this.score += points;
    }

    public void ResetScore()
    {
        this.score = 0;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
