using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartsUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsHolder playerStatsHolder;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private GameObject heartPrefab;


    void Start()
    {
        if (playerStatsHolder != null)
        { 
            playerStatsHolder.OnHealthChanged += UpdateHeartsUI;
        }

        UpdateHeartsUI();
    }

    private void OnDestroy()
    {
        if (playerStatsHolder != null)
        { 
        playerStatsHolder.OnHealthChanged -= UpdateHeartsUI;
        }
    }


    public void UpdateHeartsUI()
    {
        foreach (Transform child in heartsContainer)
        { 
        Destroy(child.gameObject);
        }

        for (int i = 0; i < playerStatsHolder.Stats.Hearts; i++)
        {
            Instantiate(heartPrefab, heartsContainer);
        
        }
    
    }

}
