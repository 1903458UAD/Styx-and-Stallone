using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Player Attributes")]
    [Range(1.0f, 3.0f)] public float moveSpeed;

    [SerializeField] private int playerIndex = 0;

    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputVector = Vector2.zero;

    private void Awake()
    {
       characterController = GetComponent<CharacterController>();
    }

    public int GetPlayerIndex()
    {
        return playerIndex; 
    }

    void Update()
    {
        Move();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    private void Move()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
