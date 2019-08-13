using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList;

    public List<GameObject> enemiesToFight;

    public enum Worlds
    {
        OverWorld,
        BattleStage
    }
    //void awke is called before void start on ANY  OBJECT
    private void Awake()
    {
        
        //this will make it so we can travel between scenes (good for keeping track of gameplay!)
        DontDestroyOnLoad(this.gameObject);
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            enemiesToFight.Add(enemyList[Random.Range(0, enemyList.Count)]);
        }
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.OverWorld:
                //load overworld
                SceneManager.LoadScene("OverWorld");
                break;

            case Worlds.BattleStage:
                //go to battle scence
                GenerateEnemies();
                SceneManager.LoadScene("BattleStage");
                break;
        }
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i ++)
        {
            enemiesToFight.Add(enemySpawnList[Random.Range(0, enemySpawnList.Count)]);
        }
    }
    
}