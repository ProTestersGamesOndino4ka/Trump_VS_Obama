using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	
	private static float gameTimer;
	private static readonly byte GAME_TIME = 10;
	private static GameTimer instance;

	public static bool isTimerStarted{ get; private set; }

	void Start()
	{
		instance = this;
	}

	IEnumerator Game()
	{
		while(true)
		{
			if(gameTimer > 0)
			{
				yield return null;
				gameTimer -= Time.deltaTime;
				LocalPresident.SetTextToStartButton(Math.Round(gameTimer, 3).ToString());
				if(EnemyPresident.GetCurrentPresidentImage() != null)
				{
					EnemyPresident.SetTextToStartButton(Math.Round(gameTimer, 3).ToString());
				}
			}
			else
			{
				LocalPresident.SetTextToStartButton("0");
				if(EnemyPresident.GetCurrentPresidentImage() != null)
				{
					EnemyPresident.SetTextToStartButton("0");
				}
				Debug.Log("CHOOSE WINNER");
				isTimerStarted = false;
				ScoreHandler.ChooseWinner();
				break;
			}
		}
	}

	public static void StartGameTimer()
	{
		gameTimer = GAME_TIME;
		instance.StartCoroutine(instance.Game());
		isTimerStarted = true;
	}

	void Update()
	{

	}
}
