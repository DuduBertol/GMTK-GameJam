using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class s_PlayerAim : MonoBehaviour
{
    [Header("Aim Settings")]
    private Vector2 _screenMousePosition;
    [SerializeField] private Vector3 _mousePosition;

     GameObject _mouseTargetPosition;
     public GameObject _mouseTargetGO;
     public LayerMask mouseLayer;

    
     [Header("Debug")] public bool debugButton;
    void Start()
    {
        _mouseTargetPosition = Instantiate(_mouseTargetGO);
         
    }

    // Update is called once per frame
    void Update()
    {
        _screenMousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(_screenMousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mouseLayer))
        {
            _mousePosition = new Vector3(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
            if (_mouseTargetPosition != null)
            {
                _mouseTargetPosition.transform.position = _mousePosition;
            }
            if(debugButton)
                Debug.DrawLine(transform.position, _mousePosition, Color.blue);
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        return _mousePosition;
    }
}
