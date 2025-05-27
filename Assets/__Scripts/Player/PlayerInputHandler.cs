using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;
using static UnityEngine.Rendering.DebugUI;

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

    public void OnJump(CallbackContext context)
    {
        if (context.started)
        {
            playerActions.SetInputJumpBool(true);
        }

        Debug.Log(context.started);
    }
}
