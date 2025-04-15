using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    [field:SerializeField] public InputAction MoveInputReference { get; private set; }
    [field:SerializeField] public InputAction GrabInputReference { get; private set; }
    [field:SerializeField] public InputAction SeparationInputReference { get; private set; }
    
    public Vector2 MoveInput {get; private set;}
    public bool GrabInput {get; private set;}
    public bool GrabInputReleased { get; private set;}
    public bool GrabInputPressed { get; private set;}
    public bool SeparationInputPressed {get; private set;}
    public bool SeparationInputReleased {get; private set;}

    public void UpdateInputs()
    {
        MoveInput = MoveInputReference.ReadValue<Vector2>();
        GrabInput = GrabInputReference.IsPressed();
        GrabInputReleased = GrabInputReference.WasReleasedThisFrame();
        GrabInputPressed = GrabInputReference.WasPressedThisFrame();
        SeparationInputPressed = SeparationInputReference.WasPressedThisFrame();
        SeparationInputReleased = SeparationInputReference.WasReleasedThisFrame();
    }
}
