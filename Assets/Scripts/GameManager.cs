using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
                SceneManager.LoadScene("BattleStage");
                break;
        }
    }

}