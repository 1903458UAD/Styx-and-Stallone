using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestructableOjectStats", menuName = "Game/Destructable Object Stats")]

public class DestructableObjectStats : ScriptableObject
{



    [SerializeField] private int maxHealth = 3;
    //[SerializeField] private int currentHealth = 3;
    [SerializeField] private int fruitsToSpawn = 5;
    [SerializeField] private GameObject fruitPrefab;

    public int MaxHealth => maxHealth;
    //public int CurrentHealth => currentHealth;
    public int FruitsToSpawn => fruitsToSpawn;
    public GameObject FruitPrefab => fruitPrefab;

    //public void ResetStats()
    //{ 
    //currentHealth = maxHealth;
    
    //}

    //public void TakeDamage(int amount)
    //{ 
    //currentHealth = Mathf.Min(currentHealth, amount);
    
    //}

    //public bool IsDestroyed => currentHealth <= 0; //if current health is < or = to 0, then it returns true


}
