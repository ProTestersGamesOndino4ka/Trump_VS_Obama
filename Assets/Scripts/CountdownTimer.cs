using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{

	private static float countdownTimer;
	private static readonly byte COUNTDOWN_TIME = 3;
	private static CountdownTimer instance;

	void Start()
	{
		instance = this;
	}

	IEnumerator Countdown()
	{
		while(true)
		{
			if(countdownTimer > 0)
			{
				yield return null;
				countdownTimer -= Time.deltaTime;
				LocalPresidentImage.SetTextToStartButton(Math.Round(countdownTimer, 0).ToString());
				EnemyPresidentImage.SetTextToStartButton(Math.Round(countdownTimer, 0).ToString());
			}
			else
			{
				GameTimer.StartGameTimer();
				break;
			}
		}
	}

	public static void StartCountdown()
	{
		countdownTimer = COUNTDOWN_TIME;
		instance.StartCoroutine(instance.Countdown());
	}

	void Update()
	{
		
	}
}
