using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    public Button cookButton;
    private Button seasonButton;
    private Button snackButton;

    public Image pHealthBarFill;
    public Image eHealthBarFill;

    public BattleManager bManager;

    //we use System. to avoid Random.Range errors (and other syntax error garbo)
    public event System.Action CallCook;
    public event System.Action CallSeason;
    public event System.Action CallSnack;

    public Text[] combatLogLines;
    public List<string> combatLog;

    //void Awake runs before void Start (on any script that is created at the same time as it)
    void Awake()
    {
        //UpdateHealthBar(true, amount);
        bManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        //original event on left, subscriber on the right!
        bManager.UpdateHealth += UpdateHealthBar;
    }


    public void UpdateHealthBar(bool isPlayer, float satiety)
    {
        //we will handle fill amount back in the respective scripts calling this function!
        if (isPlayer)
        {
            eHealthBarFill.fillAmount = satiety;
        }
        else
        {
            pHealthBarFill.fillAmount = satiety;
        }
    }

    //Since we can't call events directly from the Buttons in the UI we're just going
    //to make these functions (plus if we need to do any fancy graphics this will be
    //much easier having these anyways
    public void CallCookEvent()
    {
        UpdateCombatLog("You cooked the enemy");
        CallCook();
    }
    public void CallSeasonEvent()
    {
        UpdateCombatLog("Added Seasoning!");
        CallSeason();
    }
    public void CallSnackEvent()
    {
        UpdateCombatLog("You ate some spaghetti grass");
        CallSnack();
    }

    public void UpdateCombatLog(string incLog)
    {
        //add string to list (at position [0]! super important!)
        combatLog.Insert(0, incLog);
        //if list length is > array length, delete the last entry [last index]
        if (combatLog.Count > combatLogLines.Length)
        {
            combatLog.RemoveAt(combatLog.Count - 1);
        }
        //loop through array and set the text to the strings!
        for (int i = 0; i < combatLog.Count; i++)
        {
            combatLogLines[i].text = combatLog[i];
        }

    }
}
