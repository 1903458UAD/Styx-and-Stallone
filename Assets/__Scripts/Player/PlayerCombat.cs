using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 45f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask damageableLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            PerformAttack();

        }
    }

    private void PerformAttack()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Collider[] hits = Physics.OverlapSphere(origin, attackRange, damageableLayer);

        foreach (Collider hit in hits)
        {
            Vector3 toTarget = (hit.transform.position - origin).normalized;

            if (Vector3.Angle(direction, toTarget) <= attackAngle)
            {
                Debug.Log($"{gameObject.name} hit  {hit.name}");

                var destructable = hit.GetComponent<DestructableObject>();
                if (destructable != null)
                {
                    destructable.Damage(attackDamage);

                }


            }

        }
    }
}
