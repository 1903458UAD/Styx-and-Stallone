using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject lastTouch;
    private Transform spawnPoint;

    private LayerMask mask;

    private void Start()
    {
        mask = LayerMask.GetMask("Platform");
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.down), out hit, 2f, mask))
        {
            lastTouch = hit.transform.gameObject;
            spawnPoint = lastTouch.transform.Find("SpawnPoint");
        }
    }

    public void Respawn()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3 (spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        player.GetComponent<CharacterController>().enabled = true;
    }
}
