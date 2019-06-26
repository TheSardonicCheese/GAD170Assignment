using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> EnemyList;

    public List<GameObject> EnemySpawnList;

    
    public enum GameState
    {
        notInCombat,
        inCombat
    }
    public GameState gameState;

    public enum CombatState
    {
        playerTurn,
        enemyTurn,
        victory,
        loss
    }
    public CombatState combatState;

    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;


    void Start()
    {
       
        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyList.Add(Enemy);
        }

    }

    void Update()
    {

    }

    public void DamageEnemies()
    {
        foreach (GameObject enemy in EnemyList)
        {
            enemy.GetComponent<Stats>().satiety -= 10;
        }
    }

    public void HealEnemies()
    {
        foreach (GameObject enemy in EnemyList)
        {
            enemy.GetComponent<Stats>().satiety += 10;
        }
    }

    public void RemoveEnemy(GameObject EnemyToRemove)
    {
        EnemyList.Remove(EnemyToRemove);
    }

    public void SpawnEnemy()
    {
        //Spawning an enemy using the size of the list as the maximum of the random range
        Instantiate(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)], transform);
    }

    public void CheckCombatState()
    {
        switch (combatState)
        {
            //Player Turn
            case CombatState.playerTurn:
                //decision - attack
                //attack the enemy
                playerObj.GetComponent<Player>().AttackTarget(enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Enemy>().myStats.isDefeated)
                    SpawnEnemy();
                //next case, usually enemy turn
                break;


                //Enemy Turn
            case CombatState.enemyTurn:
                //decision - attack
                //attack the player
                enemyObj.GetComponent<Enemy>().AttackTarget(playerObj);
                //check if player is defeated
                if (enemyObj.GetComponent<Player>().myStats.isDefeated)
                {
                    //go to loss
                }
                //next case, usually player turn
                break;
               


                //Victory
                //tell player they won
                //end game

                //loss
                //tell player they lost
                //restart game
        }
    }
}
