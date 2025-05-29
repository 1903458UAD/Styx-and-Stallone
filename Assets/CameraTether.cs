using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraTether: MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float softMaxDistanceFromTether = 5.0f;
    public float hardMaxDistanceFromTether= 10.0f;

    public Camera mainCamera;
    float cameraViewWidth;

    public Vector3 offset = new Vector3(0, 5, -2.5f);
    public Vector3 offset2 = new Vector3(0, 8, -11);
    public float zoom = 0.5f;
    public float maxZoomOut = 1.0f;
    private float initialZoom;
    public float zoomChangeSpeed = 0.1f;

    void Start()
    {
        initialZoom = zoom;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;

        GetPlayerCollisionWithViewport(player1, midpoint);
        GetPlayerCollisionWithViewport(player2, midpoint);
        UpdateCameraPosition();

        CalculateTetherPosition();
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 targetPosition = transform.position + offset2;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, zoom * Time.deltaTime);
        mainCamera.transform.LookAt(transform.position);
    }

    private void CalculateTetherPosition()
    {
        transform.position = Vector3.Lerp(player1.position, player2.position, 0.5f);
    }

    private void GetPlayerCollisionWithViewport(Transform player, Vector3 midpoint)
    {
        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.position);
        Vector3 directionToMidpoint = (midpoint - player.position).normalized;
        float excess = 2.0f;

        CharacterController controller = player.GetComponent<CharacterController>();
        Vector3 moveVector = directionToMidpoint * excess;

        if (playerPos.x <= 0.1f || playerPos.x >= 0.9f || playerPos.y <= 0.1f || playerPos.y >= 0.9f)
        {
           controller.Move(moveVector * 5 * Time.deltaTime);
        }
    }
}
