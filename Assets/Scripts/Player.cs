using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentLevel;
    public float totalXP;
    public float xpRequired;
    public float xpGained;
    public Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
    }

    void XPandLvlUp()
    {
        if (currentLevel >= 5)
        {
            xpRequired = currentLevel * currentLevel + 3;
        }
        else
        {
            xpRequired = currentLevel * 3 + 4;
        }
       
        if (totalXP >= xpRequired)
        {
            currentLevel++;
            myStats.satiety += 20;
            myStats.metabolism += 10;
            myStats.hunger += 10;
            myStats.rawness += 10;
            myStats.luck += 3;
            Debug.Log("Level Up!");
        }
    }

    public void GainXPGnoc()
    {
        totalXP += 1f / (.5f * currentLevel);
        XPandLvlUp();

    }
    public void GainXPRav()
    {
        totalXP += 5f / (.5f * currentLevel);
        XPandLvlUp();
    }
    public void GainXPPer()
    {
        totalXP += 10f / (.5f * currentLevel);
        XPandLvlUp();
    }
}
