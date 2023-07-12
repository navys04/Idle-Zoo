using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private bool _isPressed = false;
    private float _vertical;

    [SerializeField] private Vector2 edges;
    [SerializeField] private float scrollSpeed = 1.0f;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isPressed = false;
        }
        
        _vertical = Input.GetAxis("Mouse Y");
        
        if (_isPressed) Scroll();
    }

    private void Scroll()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        float deltaYPos = scrollSpeed * _vertical * -1;
        float newYPos = Mathf.Clamp(transform.position.y + deltaYPos, edges.x, edges.y);
        
        
        transform.position = new Vector3(transform.position.x,
            newYPos, transform.position.z);
        
        print(_vertical);
    }
}
