using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Locator : MonoBehaviour
{
    private Vector3Int oldPos;

    private Tilemap walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindWithTag("Walls").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = walls.WorldToCell(transform.position);
        if (pos.x != oldPos.x || pos.y != oldPos.y)
        {
            Debug.Log("I'm at: " + pos);
            oldPos = pos;
        }
    }
}