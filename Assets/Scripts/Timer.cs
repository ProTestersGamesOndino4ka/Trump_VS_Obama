using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer:MonoBehaviour
{

	static float _timer;
	public static bool isTimerStarted;

	public static void StartTimer (float value)
	{
		_timer = value;
		isTimerStarted = true;
		LocalPresidentText.Refresh ();
		EnemyPresidentText.Refresh ();
	}

	void Update ()
	{
		if (_timer > 0) {
			_timer -= Time.deltaTime;
			LocalPresidentTimer.SetValue (_timer);
			EnemyPresidentTimer.SetValue (_timer);
		} else if (isTimerStarted && _timer <= 0.15) {
			isTimerStarted = false;
			LocalPresidentTimer.SetValue (0);
			EnemyPresidentTimer.SetValue (0);
			new LocalPresidentImage ().SetButtonsActiveState (true);
			ScorePanelAnimationHandler.SetEnd (GameObject.Find ("Panel_local_score").GetComponent<Animator> (), true);
			ScorePanelAnimationHandler.SetEnd (GameObject.Find ("Panel_enemy_score").GetComponent<Animator> (), true);
		}

	}
}
