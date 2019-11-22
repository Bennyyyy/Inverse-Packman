using UnityEngine;

public class MoveControls : MonoBehaviour
{
    public GameObject player;
    public float      moveSpeed = 1.0f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            direction += Vector3.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            direction += Vector3.down;

        if (Input.GetKey(KeyCode.LeftArrow))
            direction += Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction += Vector3.right;

        var totalMovement = direction * moveSpeed * Time.fixedDeltaTime;
        
        player.transform.position += totalMovement;
    }
}