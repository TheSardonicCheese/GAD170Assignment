using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    //removing enemies from list
    private GameObject gameManager;

    public Stats myStats;

    public enum EnemyTypes
    {
        gnoc,
        raveghouli,
        peorogre,
    }

    public EnemyTypes myType;


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
	

    public void Defeated()
    {
        //removing enemies from list
        gameManager.GetComponent<GameManager>().RemoveEnemy(gameObject);
    }
}
