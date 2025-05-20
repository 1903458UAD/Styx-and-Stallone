using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject cameraTarget;

    private Vector3 midPoint;

    #endregion

    private void Update()
    {
        midPoint = (player1.transform.position + player2.transform.position) /2;

        cameraTarget.transform.position = new Vector3 (midPoint.x, midPoint.y, midPoint.z);
    }
}
