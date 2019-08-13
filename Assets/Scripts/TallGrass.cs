using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrass : MonoBehaviour
{
    private GameObject gameManager;
    public List<GameObject> enemiesLibrary;

    public bool isInField;
    public enum fieldType
    {
        spaghetti,
        broccoli,
        stew
    }
    public fieldType myType;
        
    public List<GameObject> enemiesToSend;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Easy"));
        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Normal"));
        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Difficult"));
        
        RollDice();
    }

    void RollDice()
    {
        int diceRoll = Random.Range(1, 7);
        Debug.Log(diceRoll);
        if (diceRoll > 2 && isInField)
        {
            for(int i = 0; i < 3; i++)
            {
                switch (myType)
                {
                    case fieldType.spaghetti:
                        enemiesToSend.Add(enemiesLibrary[0]);
                        break;
                    case fieldType.broccoli:
                        enemiesToSend.Add(enemiesLibrary[Random.Range(0, 2)]);
                        break;
                    case fieldType.stew:
                        enemiesToSend.Add(enemiesLibrary[Random.Range(0, 3)]);
                        break;
                }
            }
            gameManager.GetComponent<GameManager>().GenerateEnemies(enemiesToSend);

            gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.BattleStage);
        }
        StartCoroutine(CheckTimer());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInField = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInField = false;
        }
    }
    IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(2);
        RollDice();
    }
}
