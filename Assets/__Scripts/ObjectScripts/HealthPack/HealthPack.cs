using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
   

    private void OnTriggerEnter(Collider other)
    {

        PlayerStatsHolder playerStatsHolder = other.GetComponentInParent<PlayerStatsHolder>();

        if (playerStatsHolder != null)
        {

            playerStatsHolder.RestoreHeart(healAmount);
            Debug.Log($"{other.name} healed {healAmount} health. Current hearts: {playerStatsHolder.Stats.Hearts}");


            gameObject.SetActive(false); //Currently set to unactivate health pack instead of destroying... Might change later
        }

    }

}
