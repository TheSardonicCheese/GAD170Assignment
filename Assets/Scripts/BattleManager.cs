using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> enemySpawnList;

    public int numOfEnemies;

    public GameState gameState;
    public CombatState combatState;


    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;
    private GameObject gameManager;
    private GameObject battleUIManager;
    public GameObject grassType;

    
    

    public enum GameState
    {
        notInCombat,
        inCombat
    }

    public enum CombatState
    {
        determineFirst,
        playerTurn,
        enemyTurn,
        victory,
        loss
    }

    public enum Decision
    {
        cook,
        season,
        snack
    }
    public Decision currentDecision;

    public event System.Action<bool, float> UpdateHealth;
    public event System.Action UpdateDecision;

    private void Awake()
    {
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallCook += CookCombat;
        battleUIManager.GetComponent<BattleUIManager>().CallSeason += SeasonCombat;
        battleUIManager.GetComponent<BattleUIManager>().CallSnack += SnackCombat;
    }


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        foreach(GameObject tempEnemy in gameManager.GetComponent<GameManager>().enemiesToFight)
        {
            enemySpawnList.Add(tempEnemy);
        }
        gameManager.GetComponent<GameManager>().enemiesToFight.Clear();

        SpawnEnemy();
    }
    

    public void RemoveEnemy(GameObject EnemyToRemove)
    {
        GameObject.Destroy(EnemyToRemove);
        enemySpawnList.RemoveAt(0);
        Debug.Log(" enemy removed");
        Destroy(EnemyToRemove);
        enemySpawnList.RemoveAt(0);
        
    }

    public void SpawnEnemy()
    {
        //Spawning an enemy using the size of the list as the maximum of the random range
        if (enemySpawnList.Count == 0)
        {
            Debug.Log(" All enemies defeated!");
        }
        else
        {
            Transform EnemySpawnLoc = GameObject.FindGameObjectWithTag("EnemySpawnLoc").transform;
            enemyObj = Instantiate(enemySpawnList[0], EnemySpawnLoc);
        }
          
    }

    void CookCombat()
    {
        currentDecision = Decision.cook;
        CheckCombatState();
    }
    void SeasonCombat()
    {
        currentDecision = Decision.season;
        CheckCombatState();
    }
    void SnackCombat()
    {
        currentDecision = Decision.snack;
        CheckCombatState();
    }

    public void GiveXP()
    {
        if (enemyObj = GameObject.FindGameObjectWithTag("Easy"))
        {
            playerObj.GetComponent<Stats>().GainXPGnoc();
            Debug.Log(" gained xp");
        }
        else if (enemyObj = GameObject.FindGameObjectWithTag("Normal"))
        {
            playerObj.GetComponent<Stats>().GainXPRav();
            Debug.Log(" gained xp");
        }
        else if (enemyObj = GameObject.FindGameObjectWithTag("Difficult"))
        {
            playerObj.GetComponent<Stats>().GainXPMon();
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
                //decision - cook, season, snack
                switch (currentDecision)
                {
                    case Decision.cook:
                        BattleRoundCook(playerObj, enemyObj);
                        break;
                    case Decision.season:
                        BattleRoundSeason(playerObj, enemyObj);
                        break;
                    case Decision.snack:
                        BattleRoundSnack(playerObj);
                        break;
                }
                   

                //attack the enemy
                BattleRoundCook(playerObj, enemyObj);
                //season enemy

                //snack
                BattleRoundHeal(playerObj);
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
                //decision - attack or snack
                //attack the player
                StartCoroutine(StartBattle());
                BattleRoundCook(enemyObj, playerObj);
                //snack
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
                if (enemySpawnList.Count == 0)
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

    public void BattleRoundCook(GameObject attacker, GameObject defender)
    {
        
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
            float percentage = defender.GetComponent<Stats>().satiety / defender.GetComponent<Stats>().maxSatiety;
            Debug.Log(attacker + " did " + percentage * 100 + " percent damage");
            UpdateHealth(attacker == playerObj, percentage);
     
    }
    public void BattleRoundSeason(GameObject attacker, GameObject defender)
    {
        defender.GetComponent<Stats>().Seasoned(attacker.GetComponent<Stats>().attackEffect);
    }
    public void BattleRoundHeal(GameObject attacker)
    {
        /*
        grassType = GameObject.FindGameObjectWithTag("GrassType");
        if (grassType.GetComponent<TallGrass>().isSpaghetti == true)
        {
            attacker.GetComponent<Stats>().satiety += 20;
        }
        else if(GetComponent<TallGrass>().isBroccoli ==true)
        {
            attacker.GetComponent<Stats>().satiety += 50;
        }
        else if (GetComponent<TallGrass>().isStew == true)
        {
            attacker.GetComponent<Stats>().satiety += 100;
        }*/
        Debug.Log("Player healed!");
    }

    public void BattleRoundSnack (GameObject attacker)
    {
        //determine what field the player is in
        //increase health accordingly
    }

    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(3f);

        CheckCombatState();
    }



}
