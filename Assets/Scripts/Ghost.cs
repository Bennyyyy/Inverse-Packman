using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public float            moveSpeed        = 1.0f;
    public MoveDirection    currentDirection = MoveDirection.Idle;
    public List<Vector3Int> movementPath     = new List<Vector3Int>();
    public Tilemap          tilemap;
    public Grid             grid;
    public SpriteRenderer   spriteRenderer;
    public GameObject       player;

    private void FixedUpdate()
    {
        if (InvertPacman.gameState != GameState.Play)
            return;

        currentDirection = GetNextDirection();

        var totalMovement = currentDirection.AsVector() * moveSpeed * Time.fixedDeltaTime;

        transform.position += totalMovement;
    }

    private MoveDirection GetNextDirection()
    {
        var myPosition = grid.WorldToCell(transform.position);
        var next       = GetNext();

        if (myPosition.Equals(next))
        {
            PopNext();
            next = GetNext();
        }

        var diffX = next.x - myPosition.x;
        var diffY = next.y - myPosition.y;

        if (diffX == 0 && diffY == 0)
            return MoveDirection.Idle;

        var direction = MoveDirection.Idle;

        if (diffX > 0)
            direction = MoveDirection.Right;
        else
            direction = MoveDirection.Left;

        var tile = GetTileForDirection(myPosition, direction);

        if (tile != null)
        {
            if (diffY > 0)
                direction = MoveDirection.Up;
            else
                direction = MoveDirection.Down;
        }

        var tile2 = GetTileForDirection(myPosition, direction);

        if (tile2 != null)
            direction = MoveDirection.Idle;

        return direction;
    }

    private TileBase GetTileForDirection(Vector3Int myPosition, MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                myPosition += Vector3Int.left;
                break;
            case MoveDirection.Right:
                myPosition += Vector3Int.right;
                break;
            case MoveDirection.Up:
                myPosition += Vector3Int.up;
                break;
            case MoveDirection.Down:
                myPosition += Vector3Int.down;
                break;
        }

        return tilemap.GetTile(myPosition);
    }

    private void PopNext()
    {
        movementPath.RemoveAt(0);
    }

    private Vector3Int GetNext()
    {
        return movementPath.Count > 0 ? movementPath[0] : (player == null ? Vector3Int.zero : grid.WorldToCell(player.transform.position));
    }

    private void Update()
    {
        switch (currentDirection)
        {
            case MoveDirection.Left:
                spriteRenderer.flipX = true;
                //transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case MoveDirection.Right:
                spriteRenderer.flipX = false;
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}