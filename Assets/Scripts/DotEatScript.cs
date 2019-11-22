using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DotEatScript : MonoBehaviour
{
    private Tilemap dotmap;
    private int numDots;
    
    // Start is called before the first frame update
    void Start()
    {
        dotmap = gameObject.GetComponent<Tilemap>();
        
        // count the dots
        numDots = 0;
        
        for(int y=-dotmap.size.y; y<dotmap.size.y; y++)
            for(int x=-dotmap.size.x; x<dotmap.size.x; x++)
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
            
            Debug.Log("numDots: " + numDots);
            
            if (numDots == 0)
                Debug.Log("CONGRATULATIONS");
        }
    }
}
