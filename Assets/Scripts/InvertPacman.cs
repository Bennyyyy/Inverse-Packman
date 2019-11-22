using UnityEngine;

public static class InvertPacman
{
    public static GameState gameState = GameState.Play;
}

public enum GameState
{
    Intro,
    Play,
    GameOver
}