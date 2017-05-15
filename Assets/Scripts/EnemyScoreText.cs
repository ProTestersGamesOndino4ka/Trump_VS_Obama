using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScoreText : MonoBehaviour
{

	static Text _enemyPresident;

	void Start()
	{
		_enemyPresident = GetComponent<Text>();
	}

	public static void SetText(string scoreText)
	{
		_enemyPresident.text = scoreText;
	}
}
