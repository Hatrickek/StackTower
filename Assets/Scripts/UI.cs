using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameSO GameSO;
    [SerializeField] private LevelManager LevelManager;

    public List<TMP_Text> ScoreTexts = new();
    public TMP_Text LevelText;

    public GameObject MainMenu;
    public GameObject GameOverMenu;
    public GameObject LevelCompletedMenu;

    private void Start()
    {
        MainMenu.SetActive(true);
    }

    private void HandleMenus()
    {
        switch (GameSO.State)
        {
            case GameSO.GameState.Alive:
                GameOverMenu.SetActive(false);
                break;
            case GameSO.GameState.Dead:
                GameOverMenu.SetActive(true);
                break;
            case GameSO.GameState.MainMenu:
                MainMenu.SetActive(true);
                break;
            case GameSO.GameState.LevelCompleted:
                LevelCompletedMenu.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        HandleMenus();

        if (MainMenu.activeSelf && Input.GetMouseButtonDown(0))
        {
            MainMenu.SetActive(false);
        }

        if (GameOverMenu.activeSelf && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
            GameSO.ResetScore();
            GameSO.State = GameSO.GameState.Alive;
        }

        if (LevelCompletedMenu.activeSelf && Input.GetMouseButtonDown(0))
        {
            LevelCompletedMenu.SetActive(false);
            LevelManager.GenerateNewLevel();
        }

        foreach (var scoreText in ScoreTexts)
        {
            scoreText.text = GameSO.Score.ToString();
        }
        LevelText.text = GameSO.Level.ToString();
    }
}
