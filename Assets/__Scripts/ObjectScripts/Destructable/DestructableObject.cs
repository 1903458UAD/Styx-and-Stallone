using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{

    [SerializeField] private DestructableObjectStats stats;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = stats.MaxHealth;
        Debug.Log($"{gameObject.name} health reset to: {currentHealth}");
        //stats.ResetStats(); //Makes sure health full when game starts
    }

    public void Damage(int amount)
    {
        currentHealth = currentHealth - amount;

        Debug.Log($"{gameObject.name} took {amount} damage. Current Health: {currentHealth}");


        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} is destroyed");
            DestroyObject();
        
        }


        //stats.TakeDamage(amount);
        //if (stats.IsDestroyed)
        //{

        //    Debug.Log($"{gameObject.name} is destroyed");
        //    DestroyObject();
        //}
    
    }

    private void DestroyObject()
    { 
    gameObject.SetActive(false); //Doesnt destroy it just unactivates it (Easier for resetting)

        for (int i = 0; i < stats.FruitsToSpawn; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3
            (
                Random.Range(-0.5f, 0.5f),
                Random.Range(0.5f, 1f),
                Random.Range(-0.5f, 0.5f)
            );

            Instantiate(stats.FruitPrefab, spawnPos, Quaternion.identity);
        }
    
    
    }



}
