using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.position = mousePos;
    }
}
