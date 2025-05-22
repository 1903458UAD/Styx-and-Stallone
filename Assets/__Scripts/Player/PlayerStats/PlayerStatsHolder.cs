using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHolder : MonoBehaviour
{

    public event System.Action OnHealthChanged;


    [SerializeField] private PlayerStats playerStats;

    public PlayerStats Stats => playerStats;

    private void Awake() //Needed to reset HP each game instant
    {
        playerStats.ResetStats();

    }


    public void DamageHeart(int Amount)
    { 
    playerStats.DamageHeart(Amount);
    OnHealthChanged?.Invoke();
    
    }

    public void RestoreHeart(int amount)
    {
        playerStats.RestoreHeart(amount);
        OnHealthChanged?.Invoke(); 
    
    }
}
