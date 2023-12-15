using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum GameStates
{
    playing, paused, cutscene, gameOver, talking, shoping, upgrading, notificated, holded
}

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public Transform backGrounds;
    public GameObject gameElementsContainer;
    [SerializeField] int _money = 0;
    public int money
    {
        get{return _money;}
        set{
            _money = value;
        }
    }
    GameStates stateHolder;
    public GameStates gameState;
    [HideInInspector] public UnityEvent OnStartLevel;
    [HideInInspector] public UnityEvent<GameStates> OnStateEnter;
    [HideInInspector] public UnityEvent<GameStates> OnStateExit;

    private void Awake() {
        GM = this;
    }

    public void ChangeStateOfGame(GameStates newState)
    {
        //OnGameStateExit
        switch(gameState)
        {
            case GameStates.paused:
                Time.timeScale = 1;
                break;
        }

        OnStateExit?.Invoke(gameState);

        //OnGameStateEnter
        switch(newState)
        {
            case GameStates.paused:
                Time.timeScale = 0;
                stateHolder = gameState;
                break;
            case GameStates.notificated:
                stateHolder = gameState;
                break;
            case GameStates.gameOver:
                GameOver();
                break;
            case GameStates.holded:
                ChangeStateOfGame(stateHolder);
                break;
        }

        OnStateEnter?.Invoke(newState);

        gameState = newState;
    }

    void GameOver()
    {
        AudioManager.instance.Play("Death_theme");
    }
}
