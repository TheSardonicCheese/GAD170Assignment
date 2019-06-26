using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    //removing enemies from list
    private GameObject gameManager;

    public Stats myStats;
    //enemy 0 = gnoc
    //enemy 1 = raveghouli
    //enemy 2 = peorogre
    public int enemyID = 1;
    public enum EnemyTypes
    {
        gnoc,
        raveghouli,
        peorogre,
    }

    public EnemyTypes myType;


	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

		myStats = GetComponent<Stats>();

        switch (myType)
        {
            case EnemyTypes.gnoc:
                //do setup
                break;
            case EnemyTypes.raveghouli:
                //do thing
                break;
            case EnemyTypes.peorogre:
                //do thing
                break;

        }

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
        Target.GetComponent<Player>().Attacked(myStats.hunger,Stats.StatusEffect.none);
    }

    public void Defeated()
    {
        //removing enemies from list
        gameManager.GetComponent<GameManager>().RemoveEnemy(gameObject);
    }
}
