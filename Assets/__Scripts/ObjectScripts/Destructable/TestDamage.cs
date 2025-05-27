using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public DestructableObject destructable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Damage input logged");

            if (destructable != null)
            {
                destructable.Damage(1);
            }
            else
            {
                Debug.LogWarning("DestructableObject not assigned!");
            }


        }
    }



}
