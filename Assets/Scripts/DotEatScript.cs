using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DotEatScript : MonoBehaviour
{
    public GameController gameController;

    private Tilemap dotmap;
    private int     numDots;

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

            dotmap.GetComponent<AudioSource>().Play();

            Debug.Log("numDots: " + numDots);

            if (numDots == 0)
                gameController.ShowGameOver(true, "PACMAN WINS");
        }
    }
}
