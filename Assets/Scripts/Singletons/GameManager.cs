using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static GameManager Instance { get; private set; }    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeGameState(GameState.GameStart);
    }

    void ChangeGameState(GameState state)
    {
        switch(state)
        {
            case GameState.None:
                break;
            case GameState.GameStart:
                StartGame();
                break;
            case GameState.GameFinished:
                break;            
        }
    }

    void StartGame()
    {
        OnGameStart();

        // Spawn Entities
        
    }


    enum GameState
    {
        GameStart,
        GameFinished,
        None
    }
    
    public static event Action OnGameStart;
    public static event Action OnGameFinished;

}
