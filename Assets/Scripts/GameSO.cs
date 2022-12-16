using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Game Data", menuName = "ScriptableObjects/Game Data", order = 1)]
public class GameSO : ScriptableObject
{
    public enum GameState
    {
        Alive,
        Dead,
        MainMenu,
        LevelCompleted
    }

    public GameState State;
    public int Score;
    public int Level;
    public int PlatformAmount = 15;

    private void OnEnable() {
        Score = 0;    
    } 
    public void AddScore()
    {
        Score++;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void AddLevel()
    {
        State = GameState.LevelCompleted;
        Level++;
        PlatformAmount += Level / 2;
    }

    [Button("Clear Data")]
    public void ClearData()
    {
        Score = 0;
        Level = 0;
        PlatformAmount = 15;
        State = GameState.Alive;
    }
}
