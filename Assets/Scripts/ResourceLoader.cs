using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    
    private Color[] ghostColors = {new Color(1.0f, 0.0f, 0.0f), 
                                   new Color(1.0f, 0.7f, 0.0f),
                                   new Color(0.0f, 0.75f, 1.0f),
                                   new Color(0.0f, 1.0f, 0.25f)
                                   };

    private int colorIdx = 0;
    
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
        gameObj.GetComponent<SpriteRenderer>().color = getNextColor();
        return gameObj;
    }
    
    private Color getNextColor()
    {
        Color color = ghostColors[colorIdx % ghostColors.Length];
        colorIdx++;
        
        return color;
    }
}
