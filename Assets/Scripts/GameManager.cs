using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList;

    public List<GameObject> enemiesToFight;

    public int numOfEnemies; 

    
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

        for (int i = 0; i < numOfEnemies; i ++)
        {
            GameObject spawnedEnemy = Instantiate(enemyList[Random.Range(0, enemyList.Count)], transform);
            enemiesToFight.Add(spawnedEnemy);
        }

        SpawnEnemy();
        CheckCombatState();
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

   

    public void RemoveEnemy(GameObject EnemyToRemove)
    {
        Debug.Log(" enemy removed");
        enemiesToFight.Remove(EnemyToRemove);
        Destroy(EnemyToRemove);
        
    }

    public void SpawnEnemy()
    {
        //Spawning an enemy using the size of the list as the maximum of the random range
    
            enemyObj = enemiesToFight[Random.Range(0, enemiesToFight.Count)];
            Debug.Log(" A" + enemyObj + " appeared! ");        
          
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
                    Debug.Log(enemyObj + " defeated!");
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
                    Debug.Log("Game Over");
                    combatState = CombatState.loss;
                    break;
                }
                combatState = CombatState.playerTurn;
                //next case, usually player turn
                break;


            case CombatState.victory:
                if (enemiesToFight.Count == 0)
                {
                    Debug.Log(" All Enemies Defeated!");
                    break;
                }
                else
                {
                    Debug.Log(" The fight isn't over yet!");
                    combatState = CombatState.playerTurn;
                    break;
                }



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
