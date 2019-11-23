using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IPointerHandler : MonoBehaviour
{
    private Vector3        startPositionVector3;
    private Vector3        currentPositionVector3;
    public  Tilemap        tileMap = null;
    public  Grid           gridMap = null;
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

            ghost.SetMovementPath(path);

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
    }
}
