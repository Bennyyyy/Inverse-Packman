using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 pacManStartPosition;

    private GameObject       _pacMan;
    private List<GameObject> _ghostList = new List<GameObject>();

    [HideInInspector] public ResourceLoader _loader;

    // Start is called before the first frame update
    void Start()
    {
        _loader = gameObject.AddComponent<ResourceLoader>();

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Reset()
    {
        Destroy(_pacMan);
        foreach (var ghost in _ghostList.ToArray())
        {
            Destroy(ghost);
        }

        _ghostList.Clear();


        // TODO: Rebuild Grid

        _pacMan = _loader.LoadPacMan(pacManStartPosition);


        var newGhost = _loader.LoadGhost(new Vector3(3.12f, 1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(-3.12f, 1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(3.12f, -1.68f, 0f));
        _ghostList.Add(newGhost);
        newGhost = _loader.LoadGhost(new Vector3(-3.12f, -1.68f, 0f));
        _ghostList.Add(newGhost);
    }
}