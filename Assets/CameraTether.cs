using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    public Vector3 offset = new Vector3(0, 15, -5); 
    public float zoom = 0.5f;
    public float maxZoomOut = 1.0f;
    private float initialZoom;
    public float zoomChangeSpeed = 0.1f;

    void Start()
    {

        // Set initial zoom
        initialZoom = zoom;

    }

    // Update is called once per frame
    void Update()
    {
        CalculateTetherPosition();
        CalculateZoom();
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 targetPosition = transform.position + offset;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, zoom * Time.deltaTime);
        mainCamera.transform.LookAt(transform.position);
    }

    private void CalculateTetherPosition()
    {
        transform.position = Vector3.Lerp(player1.position, player2.position, 0.5f);
    }

    private void CalculateZoom()
    {

        Vector3 topRightWorldPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10));
        Vector3 bottomRightWorldPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 10));
        Vector3 bottomLeftWorldPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 topLeftWorldPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 10));

        GetPlayerCollisionWithViewport(player1);
        GetPlayerCollisionWithViewport(player2);

        //float player1Distance = CalculateDistanceBetweenObjects(transform, player1);
        //float player2Distance = CalculateDistanceBetweenObjects(transform, player2);

        //if (player1Distance >= softMaxDistanceFromTether || player2Distance >= softMaxDistanceFromTether)
        //{
        //    if(player1Distance >= hardMaxDistanceFromTether || player2Distance >= hardMaxDistanceFromTether)
        //    {
        //        return;
        //    }


        //        zoom += zoomChangeSpeed * Time.deltaTime;

        //}

        //else
        //{
        //    if (zoom > initialZoom)
        //    {
        //        zoom -= zoomChangeSpeed * Time.deltaTime;
        //    }
        //}

        //// Zoom out if players reach end of camera width
        //if (cameraViewWidth <  10* CalculateDistanceBetweenPlayers())
        //{
        //    zoom += zoomChangeSpeed * Time.deltaTime;
        //}

        //else
        //{
        //    // Zoom in if players are within camera width and the zoom is higher than the initial zoom
        //    if(zoom > initialZoom)
        //    {
        //        zoom -= zoomChangeSpeed * Time.deltaTime;
        //    }
        //}
    }

    private void GetPlayerCollisionWithViewport(Transform player)
    {
        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.position);

        // Player has collided with far right of viewport
        if(playerPos.x <= 0)
        {
            Debug.Log("Far Right Viewport Collision");
        }

        // Player has collided with far left of viewport
        if(playerPos.x >= 1)
        { 
            Debug.Log("Far Left Viewport Collision");
        }

        // Player has collided with bottom of viewport
        if(playerPos.y <= 0)
        {
            Debug.Log("Far Bottom Viewport Collision");
        }

        // Player has collided with top of viewport
        if(playerPos.y >= 1)
        {
            Debug.Log("Far Top Viewport Collision");
        }
    }

    private float CalculateDistanceBetweenObjects(Transform object1, Transform object2)
    {
        return Vector3.Distance(object1.position, object2.position);
    }
}
