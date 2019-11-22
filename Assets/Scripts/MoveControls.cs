using System;
using UnityEngine;

public class MoveControls : MonoBehaviour
{
    public GameObject    player;
    public float         moveSpeed        = 1.0f;
    public MoveDirection currentDirection = MoveDirection.Idle;

    // Update is called once per frame
    private void FixedUpdate()
    {
        var pressedDirection = MoveDirection.Idle;

        if (Input.GetKey(KeyCode.UpArrow))
            pressedDirection = MoveDirection.Up;
        else if (Input.GetKey(KeyCode.DownArrow))
            pressedDirection = MoveDirection.Down;

        if (Input.GetKey(KeyCode.LeftArrow))
            pressedDirection = MoveDirection.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            pressedDirection = MoveDirection.Right;

        if (pressedDirection != MoveDirection.Idle)
            currentDirection = pressedDirection;

        var totalMovement = currentDirection.AsVector() * moveSpeed * Time.fixedDeltaTime;

        player.transform.position += totalMovement;
    }

    private void Update()
    {
        switch (currentDirection)
        {
            case MoveDirection.Left:
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case MoveDirection.Right:
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case MoveDirection.Up:
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case MoveDirection.Down:
                transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
}

public enum MoveDirection
{
    Idle,
    Left,
    Right,
    Up,
    Down
}

static class MoveDirectionMethods
{
    public static Vector3 AsVector(this MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                return Vector3.left;
            case MoveDirection.Right:
                return Vector3.right;
            case MoveDirection.Up:
                return Vector3.up;
            case MoveDirection.Down:
                return Vector3.down;
        }

        return Vector3.zero;
    }
}