using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool doBattle = true;


    void Start()
    {

        if (GetComponent<XPandLvlUp>().currentLevel <= 3)
        {
            foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Easy"))
            {
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
            }
        }
        if(GetComponent<XPandLvlUp>().currentLevel > 3 && GetComponent<XPandLvlUp>().currentLevel < 7)
        {
            foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Easy" + "Normal"))
            {
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
            }
        }
        if (GetComponent<XPandLvlUp>().currentLevel > 7)
        {
            foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Easy" + "Normal"))
            {
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
                EnemyList.Add(Enemy);
            }
        }

    }

    void Update()
    {
        if (doBattle)
        {
            //set turn order
            StartCoroutine(battleGo());
            doBattle = false;
        }
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
        Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], transform);
        GameObject enemyObj = EnemyList[Random.Range(0, EnemyList.Count)];
    }

    public void CheckCombatState()
    {
        switch (combatState)
        {
            //Player Turn
            case CombatState.playerTurn:
                //decision - attack
                //attack the enemy
                BattleRound(playerObj, enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                {
                    RemoveEnemy(enemyObj);
                    SpawnEnemy();
                    combatState = CombatState.victory;
                    break;
                }
                //next case, usually enemy turn
                combatState = CombatState.enemyTurn;
                break;


                //Enemy Turn
            case CombatState.enemyTurn:
                //decision - attack
                //attack the player
                BattleRound(enemyObj, playerObj);
                //check if player is defeated
                if (playerObj.GetComponent<Stats>().isDefeated)
                {
                    //go to loss
                    combatState = CombatState.loss;
                    Debug.Log("Game Over");
                    break;
                }
                combatState = CombatState.playerTurn;
                //next case, usually player turn
                break;


            case CombatState.victory:
                Debug.Log("Enemy Defeated!");
                combatState = CombatState.playerTurn;
                break;

            case CombatState.loss:
                SceneManager.LoadScene("Game Over");
                break;

               
               


                
        }

    }

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //need to add miss chance
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().hunger, Stats.StatusEffect.none);
        Debug.Log(attacker.name +
            " attacks " +
            defender.name +
            " for " +
            (attacker.GetComponent<Stats>().hunger - (attacker.GetComponent<Stats>().hunger * (100 / (defender.GetComponent<Stats>().rawness + 100)))) +
            " damage");
    }

    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }

}
