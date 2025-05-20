using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollection : MonoBehaviour
{

    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            GameManager.Instance.bananasP1 += value;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player2"))
        {
            GameManager.Instance.bananasP2 += value;
            Destroy(gameObject);
        }
    }
}
