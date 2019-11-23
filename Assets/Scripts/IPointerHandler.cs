using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IPointerHandler : MonoBehaviour
{
    private Vector3        startPositionVector3;
    private Vector3Int     startPositionVector3Int;
    private Vector3        dropPositionVector3;
    private Vector3Int     dropPositionVector3Int;
    private Vector3        currentPositionVector3;
    private Vector3Int     currentPositionVector3Int;
    private Plane          _groundPlane;
    public  Tilemap        tileMap = null;
    public  Grid           gridMap = null;
    public  List<Vector3>  availablePlaces;
    private PathCalculator _pathCalculator;
    public  Ghost          ghost;

    /*private void OnMouseDown()
    {
        startPositionVector3    = Input.mousePosition;
        startPositionVector3Int = Vector3Int.FloorToInt(startPositionVector3);
    }*/

    /* private void OnMouseUp()
     {
         dropPositionVector3    = Input.mousePosition;
         dropPositionVector3Int = Vector3Int.FloorToInt(dropPositionVector3);
     }*/

    private void OnMouseDrag()
    {
        try
        {
            startPositionVector3   = Input.mousePosition;
            currentPositionVector3 = transform.position;

            var startGridPos = gridMap.WorldToCell(currentPositionVector3);
            var worldPos     = Camera.main.ScreenToWorldPoint(startPositionVector3);
            var gridPos      = gridMap.WorldToCell(worldPos);

            Debug.Log("Current Mouse on Grid " + gridPos);
            Debug.Log("Ghost start Point " + startGridPos);

            var pathAsString = "";

            var path = _pathCalculator.CalculatePath(startGridPos, gridPos);
            foreach (var step in path)
            {
                pathAsString += step.ToString();
            }

            ghost.movementPath = path;

            Debug.Log(ghost.movementPath.ToString());
        }
        catch (Exception ignore)
        {
            // nothing to do
        }
    }

    void Start()
    {
        _pathCalculator = new PathCalculator(GameObject.FindWithTag("Walls").GetComponent<Tilemap>());
        tileMap         = transform.GetComponent<Ghost>().tilemap;
        gridMap         = transform.GetComponent<Ghost>().grid;
        _groundPlane    = new Plane(Vector3.up, Vector3.zero);
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int) tileMap.transform.position.y));
                Vector3    place      = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                    availablePlaces.Add(place);
            }
        }
    }
}