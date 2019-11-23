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
    public List<Vector3Int> path       = new List<Vector3Int>();
    public float            xOffset    = 0.08f;
    public float            yOffset    = 0.08f;
    public float            drawWidth  = 0.05f;

    private Grid         _grid;
    private LineRenderer _lineRenderer;

    private bool isVanishing;

    // Start is called before the first frame update
    void Start()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldDraw)
        {
            Draw();
        }

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
        }

        _lineRenderer.positionCount = path.Count;
        _lineRenderer.startWidth    = drawWidth;
        _lineRenderer.endWidth      = drawWidth;


        for (int i = 0; i < path.Count; i++)
        {
            var pos = _grid.CellToWorld(path[i]);
            pos.x += xOffset;
            pos.y += yOffset;
            _lineRenderer.SetPosition(i, pos);
        }

        if (!isVanishing)
        {
            StartCoroutine(VanishPath(1));
        }
    }

    public void Reset()
    {
        _lineRenderer.positionCount = 0;
        path.Clear();
    }

    private IEnumerator VanishPath(int seconds)
    {
        isVanishing = true;

        while (path.Count > 0)
        {
            path.Remove(path.First());
            Draw();
            yield return new WaitForSeconds(seconds);
        }

        isVanishing = false;
    }
}