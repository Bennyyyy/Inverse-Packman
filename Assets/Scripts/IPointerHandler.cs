using UnityEngine;

public class IPointerHandler : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 dropPosition;
    private Vector3 currentPosition;

    private void OnMouseDown()
    {
        startPosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        dropPosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        currentPosition = Input.mousePosition;
    }
}