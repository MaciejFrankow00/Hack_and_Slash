using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "HnS/Statistics", fileName = "Statistics")]
public class Statistics : ScriptableObject
{
    public UnityEvent LevelUp;
    public UnityEvent StatPointsChanged;
    public int currentLvl = 1;
    [field: SerializeField] public int CurrentXP {get; private set;}
    public float baseMovementSpeed = 200f;
    public float baseMaxHealth = 10f;

    public float currentStamina;
    public float baseMaxStamina = 50f;
    public float baseStaminaRegen = 2f;
    public float baseMaxMana = 50f;
    public float baseArmor = 10f;
    
    public int xpOnDeath = 10;

    public int baseStatPointsToDistribute = 0;

    public Dictionary<BaseStatType, int> StatPoints = new Dictionary<BaseStatType, int>()
    {
        {BaseStatType.STRENGTH, 1}, //MELEE DMG
        {BaseStatType.DEXTERITY, 1}, //MOVE SPEED
        {BaseStatType.VITALITY, 1}, //MAX HP
        {BaseStatType.WISDOM, 1} //SPELL DMG (WHEN WE DO THEM)
    };

    public void Init()
    {
        currentStamina = baseMaxStamina;
    }

    public int GetLevelXP(int level)
    {
        return (int)((level - 1) * 100f);
    }

    public void AddXP(int xp)
    {
        CurrentXP += xp;

        if(CurrentXP >= GetLevelXP(currentLvl + 1))
        {
            currentLvl ++;
            Debug.Log("You reached lvl: " + currentLvl);
            baseStatPointsToDistribute += 5;
            CurrentXP -= GetLevelXP(currentLvl);
            LevelUp?.Invoke();
        }
        else
        {
            Debug.Log("Level progress: " + CurrentXP + "/" + GetLevelXP(currentLvl+1) + " xp");
        }
    }
    

}

public enum BaseStatType
{
    STRENGTH,
    DEXTERITY,
    VITALITY,
    WISDOM
}
