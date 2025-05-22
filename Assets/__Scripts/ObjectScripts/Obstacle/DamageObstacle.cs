using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {

        PlayerStatsHolder playerStatsHolder = other.GetComponentInParent<PlayerStatsHolder>();

        if (playerStatsHolder != null)
        {
            playerStatsHolder.DamageHeart(damageAmount);
            Debug.Log($"{other.name} took {damageAmount} damage. Remaining hearts: {playerStatsHolder.Stats.Hearts}");
        
        }
      
    }
}
