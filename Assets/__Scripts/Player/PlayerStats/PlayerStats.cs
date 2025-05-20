using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "PlayerStats", menuName = "Game/PlayerStats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private int maxHearts = 5; //Placeholder amount
    [SerializeField] private int hearts; // Assigned in the individual characters stats (In the PlayerStats Folder)
    [SerializeField] private int attack; //Ditto^

    public int Hearts => hearts;
    public int Attack => attack;
    public int MaxHearts => maxHearts;

    public void DamageHeart(int amount)
    {

        hearts = Mathf.Max(0, hearts - amount);  //Caps hearts at zero (cant go negative)

    }

    public void RestoreHeart(int amount)
    {
        hearts = Mathf.Min(maxHearts, hearts + amount);
    }

}
