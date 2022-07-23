using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMouse : MonoBehaviour
{

    [SerializeField]
    private InputActionReference pointerPosition;

    private void Update()
    {

        transform.position = GetPointerPosition();

    }

    private Vector2 GetPointerPosition()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
