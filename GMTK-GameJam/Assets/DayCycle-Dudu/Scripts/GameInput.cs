using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    public event EventHandler OnDropAction;
    public event EventHandler OnInteractAction;

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        
        playerInputActions.Player.Enable();

        playerInputActions.Player.Drop.performed += Drop_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
    }


    private void OnDestroy() 
    {
        playerInputActions.Player.Drop.performed -= Drop_performed;

        playerInputActions.Dispose();
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Drop_performed(InputAction.CallbackContext obj)
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