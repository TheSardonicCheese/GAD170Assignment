using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPandLvlUp : MonoBehaviour 
{
public int currentLevel = 1;
public float totalXP = 0;
public float xpRequired;
public float xpGained;


	void Update () 
	{
		 
		if (currentLevel >= 5)
		{
			xpRequired = currentLevel * currentLevel + 3;
		}
		else
		{
			xpRequired = currentLevel * 3 + 4;
		}

		if(Input.GetKeyDown("x"))
		{
			//placeholder if statement until defeat small enemy function works
			totalXP += 1f/(.5f * currentLevel);
			print("+ xp");
		}
		if(Input.GetKeyDown("c"))
		{
			//placeholder if statement until defeat medium enemy function works
			totalXP += 5f/(.5f * currentLevel);
			print("+ xp");
		}
		if(Input.GetKeyDown("z"))
		{
			//placeholder if statement until defeat large enemy function works
			totalXP += 10f/(.5f * currentLevel);
			print("+ xp");
		}
		if(totalXP >= xpRequired)
		{
			currentLevel ++;
			GetComponent<Player>().myStats.satiety += 10;
			GetComponent<Player>().myStats.metabolism += 5;
			GetComponent<Player>().myStats.hunger += 5;
			GetComponent<Player>().myStats.rawness += 5;
			GetComponent<Player>().myStats.luck += 1;
			Debug.Log("Level Up!");
		}
	}
}
