using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverGameObject;
    public Vector3    pacManStartPosition;

    private GameObject       _pacMan;
    private List<GameObject> _ghostList = new List<GameObject>();
    private Text scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
    
    public  Grid             grid;
    public  Tilemap          tilemap;

    public int score = 0;

    [HideInInspector] public ResourceLoader _loader;

    private void Start()
    {
        _loader = gameObject.AddComponent<ResourceLoader>();
        Reset();
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
    
    public void Update()
    {
        if (InvertPacman.gameState == GameState.GameOver)
            if (Input.GetButton("Fire1"))
                ResetGame();

        scoreText.text = "Score:\n" + score;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        Destroy(_pacMan);
        foreach (var ghost in _ghostList.ToArray())
        {
            Destroy(ghost);
        }

        _ghostList.Clear();


        // TODO: Rebuild Grid

        _pacMan = _loader.LoadPacMan(pacManStartPosition);


        var newGhost = _loader.LoadGhost(new Vector3(3.12f, 1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(-3.12f, 1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(3.12f, -1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(-3.12f, -1.68f, 0f));
        _ghostList.Add(newGhost);

        foreach (var ghost in _ghostList)
        {
            ghost.GetComponent<Ghost>().grid    = grid;
            ghost.GetComponent<Ghost>().tilemap = tilemap;
            ghost.GetComponent<Ghost>().player = _pacMan;
        }
    }
}
