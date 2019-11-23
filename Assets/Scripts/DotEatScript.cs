using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DotEatScript : MonoBehaviour
{
    public Text           gameOverText;
    public GameController gameController;

    private Tilemap dotmap;
    private int     numDots;

    private const int SCORE_PER_DOT = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        dotmap = gameObject.GetComponent<Tilemap>();

        // count the dots
        numDots = 0;

        for (int y = -dotmap.size.y; y < dotmap.size.y; y++)
        for (int x = -dotmap.size.x; x < dotmap.size.x; x++)
            if (dotmap.GetTile(new Vector3Int(x, y, 0)) != null)
                numDots++;

        Debug.Log("numDots: " + numDots);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Pacman");

        Grid grid = dotmap.layoutGrid;

        if (dotmap.GetTile(grid.WorldToCell(player.transform.position)) != null)
        {
            dotmap.SetTile(grid.WorldToCell(player.transform.position), null);
            numDots--;
            //ResourceLoader.LoadExplosion(player.transform.position);

            gameController.score += SCORE_PER_DOT;

            dotmap.GetComponent<AudioSource>().Play();

            Debug.Log("numDots: " + numDots);

            if (numDots == 0)
            {
                gameOverText.text = "WELL DONE";
                gameController.ShowGameOver(true);
            }
        }
    }
}