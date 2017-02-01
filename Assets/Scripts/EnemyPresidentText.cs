using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresidentText : MonoBehaviour {

	static Text _enemyPresident;
	public static  int scoreValue;

	void Start()
	{
		_enemyPresident = GetComponent<Text> ();
		scoreValue = 0;
		_enemyPresident.text = scoreValue.ToString();
	}
	public static void Refresh()
	{
		scoreValue = 0;
		SetValue ();
	}
    public static void SetValue()
    {
        _enemyPresident.text = scoreValue.ToString();
    }

    public static void IncreaseValue()
	{
		_enemyPresident.text = (++scoreValue).ToString ();
	}
}
