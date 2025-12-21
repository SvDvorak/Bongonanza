using System;
using System.Linq;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

public class BongoInput : MonoBehaviour
{
    public event Action LeftPressed;
    public event Action RightPressed;
    public event Action ClapPressed;
    
    [SerializeField, Self] PlayerInput Input;

    public void OnLeftPressed(InputAction.CallbackContext ctx) => HandleInput(ctx, LeftPressed);
    public void OnRightPressed(InputAction.CallbackContext ctx) => HandleInput(ctx, RightPressed);
    public void OnClapPressed(InputAction.CallbackContext ctx) => HandleInput(ctx, ClapPressed);

    private void HandleInput(InputAction.CallbackContext ctx, Action pressed)
    {
        if(ctx.performed)
            pressed?.Invoke();
    }

    public static BongoInput GetByIndex(int playerIndex)
    {
        return FindObjectsByType<BongoInput>(FindObjectsSortMode.None)
            .Single(x => x.Input.playerIndex == playerIndex);
    }
}