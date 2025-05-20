using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerActions playerActions;

    [System.Obsolete]
    private void Awake()
    {
       playerInput = GetComponent<PlayerInput>();
       var players = FindObjectsOfType<PlayerActions>();
       var index = playerInput.playerIndex;
        playerActions = players.FirstOrDefault(p => p.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        playerActions.SetInputVector(context.ReadValue<Vector2>());
    }
}
