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
		side_0Text.Refresh ();
		side_1Text.Refresh ();
	}

	 void Update () {
		if (_timer > 0) {
			_timer -= Time.deltaTime;
			side_0Timer.SetValue (_timer);
			side_1Timer.SetValue (_timer);
		} else {
			isTimerStarted = false;
		}
		side_1Text.SetValue();

    }
}
