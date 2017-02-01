using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	static float _timer;
	public static bool isTimerStarted;
	
	public static void StartTimer(float value)
	{
		_timer = value;
		isTimerStarted = true;
		LocalPresidentText.Refresh ();
		EnemyPresidentText.Refresh ();
	}

	 void Update () {
		if (_timer > 0) {
			_timer -= Time.deltaTime;
			LocalPresidentTimer.SetValue (_timer);
			EnemyPresidentTimer.SetValue (_timer);
		} else {
			isTimerStarted = false;
		}
		EnemyPresidentText.SetValue();

    }
}
