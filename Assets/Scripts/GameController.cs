using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverGameObject;

    private void Start()
    {
        ShowGameOver(false);
    }

    public void ShowGameOver(bool show)
    {
        gameOverGameObject.SetActive(show);

        if (show)
            InvertPacman.gameState = GameState.GameOver;
        else
            InvertPacman.gameState = GameState.Play;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}