using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
    }

    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.satiety -= incDmg - myStats.rawness;
        myStats.myStatus = incEffect;
        if (myStats.satiety <= 0)
            myStats.isDefeated = true;
    }

    public void AttackTarget(GameObject Target)
    {
        Target.GetComponent<Enemy>().Attacked(myStats.hunger, Stats.StatusEffect.none);
    }
}
