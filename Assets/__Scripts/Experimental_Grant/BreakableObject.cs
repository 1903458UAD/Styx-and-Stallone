using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject prefab;
    public int bananaCount;
    public float explosionForce;
    #endregion

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            BreakObject();
        }
    }

    public void BreakObject()
    {
        for (int i = 0; i < bananaCount; i++)
        {
            GameObject banana = Instantiate(prefab, transform.position, Quaternion.identity);

            Rigidbody rb = banana.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
            }
        }
    }
}
