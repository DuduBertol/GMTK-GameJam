using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    public event EventHandler OnDropAction;

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        
        playerInputActions.Player.Enable();

        playerInputActions.Player.Drop.performed += Drop_performed;
    }

    private void OnDestroy() 
    {
        playerInputActions.Player.Drop.performed -= Drop_performed;

        playerInputActions.Dispose();
    }

    private void Drop_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDropAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalizedPlayer()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }


}