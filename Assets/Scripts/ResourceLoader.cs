using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    
    public GameObject LoadPacMan(Vector3 position)
    {
        var gameObj = GameObject.Instantiate(Resources.Load<GameObject>("PacMan"));
        gameObj.transform.position = position;
        gameObj.SetActive(true);
        return gameObj;
    }

    public GameObject LoadGhost(Vector3 position)
    {
        var gameObj = GameObject.Instantiate(Resources.Load<GameObject>("Ghost"));
        gameObj.transform.position = position;
        gameObj.SetActive(true);
        return gameObj;
    }
}
