using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
    private GameObject gameManager;

    public bool isInField;



    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        RollDice();
    }

    void RollDice()
    {
        int diceRoll = Random.Range(1, 7);
        Debug.Log(diceRoll);
        if (diceRoll > 3 && isInField)
        {
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
