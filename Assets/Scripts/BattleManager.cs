using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
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
        determineFirst,
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
        if (enemiesToFight.Count == 0)
        {
            Debug.Log(" All enemies defeated!");
        }
        else
        {
            enemyObj = enemiesToFight[Random.Range(0, enemiesToFight.Count)];
            Debug.Log(" A" + enemyObj + " appeared! ");
        }
          
    }

    public void GiveXP()
    {
        if (enemyObj = GameObject.FindGameObjectWithTag("Easy"))
        {
            playerObj.GetComponent<Player>().GainXPGnoc();
            Debug.Log(" gained xp");
        }
        else if (enemyObj = GameObject.FindGameObjectWithTag("Normal"))
        {
            playerObj.GetComponent<Player>().GainXPRav();
            Debug.Log(" gained xp");
        }
        else if (enemyObj = GameObject.FindGameObjectWithTag("Difficult"))
        {
            playerObj.GetComponent<Player>().GainXPPer();
            Debug.Log(" gained xp");
        }
    }

    public void CheckCombatState()
    {
        switch (combatState)
        {
            case CombatState.determineFirst:
                if (enemyObj.GetComponent<Stats>().metabolism <= playerObj.GetComponent<Stats>().metabolism)
                {
                    combatState = CombatState.playerTurn;
                    break;
                }
                combatState = CombatState.enemyTurn;
                break;
                            //Player Turn
            case CombatState.playerTurn:
                //decision - attack
                //attack the enemy
                BattleRound(playerObj, enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                {
                    Debug.Log(enemyObj + " defeated!");
                    GiveXP();
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
                    Debug.Log(" You win!");
                    SceneManager.LoadScene("OverWorld");
                    break;
                }
                else
                {
                    Debug.Log(" The fight isn't over yet!");
                    combatState = CombatState.determineFirst;
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
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().hunger, 
            Stats.StatusEffect.none, 
            attacker.GetComponent<Stats>().dexterity, 
            attacker.GetComponent<Stats>().luck);

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
