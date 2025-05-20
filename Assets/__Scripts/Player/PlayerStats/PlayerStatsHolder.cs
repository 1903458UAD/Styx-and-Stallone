using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHolder : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    public PlayerStats Stats => playerStats;
}
