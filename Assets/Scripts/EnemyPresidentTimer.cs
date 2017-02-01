using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresidentTimer : MonoBehaviour {

	private static Text _enemyPresidentTimer;

	void Start()
	{
		_enemyPresidentTimer = GetComponent<Text> ();
	}
	public static void SetValue(float value)
	{
		if (value < 0) {
			_enemyPresidentTimer.text = "0";
		} else {
			_enemyPresidentTimer.text = value.ToString ();
		}
	}
}
