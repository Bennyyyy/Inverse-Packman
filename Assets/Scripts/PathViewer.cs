using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class PathViewer : MonoBehaviour
{
    public bool             shouldDraw = false;
    public bool shouldReset = false;
    public List<Vector3Int> path;
    public float            xOffset    = 0.08f;
    public float            yOffset    = 0.08f;
    public float            drawWidth  = 0.05f;
    public Material material;

    private Grid         _grid;
    private LineRenderer _lineRenderer;
    private Ghost _ghost;

    // Start is called before the first frame update
    void Start()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
        _ghost = GetComponent<Ghost>();

        path = _ghost.movementPath;
    }

    // Update is called once per frame
    void Update()
    {
        path = _ghost.movementPath;
        Draw();
        

        if (shouldReset)
        {
            Reset();
        }
    }

    public void Draw()
    {
        if (_lineRenderer == null)
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            
            _lineRenderer.startWidth = drawWidth;
            _lineRenderer.endWidth   = drawWidth;
            _lineRenderer.material = material;
            var color = _ghost.GetComponent<SpriteRenderer>().color;
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
        }

        _lineRenderer.positionCount = path.Count;
        


        for (int i = 0; i < path.Count; i++)
        {
            var pos = _grid.CellToWorld(path[i]);
            pos.x += xOffset;
            pos.y += yOffset;
            _lineRenderer.SetPosition(i, pos);
        }
    }

    public void Reset()
    {
        _lineRenderer.positionCount = 0;
        path.Clear();
    }
}