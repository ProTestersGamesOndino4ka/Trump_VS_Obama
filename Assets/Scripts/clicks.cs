using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clicks : MonoBehaviour {

	public GameObject stratButton;

	public void OnSide0Click()
	{
		
		if (Timer.isTimerStarted) {
			side_0Text.IncreaseValue ();
			GoogleAutho.Message ();
		} 
		else
		{
			stratButton.SetActive(true);
		}
        
	}

	public void StartGame()
	{
		if (!Timer.isTimerStarted) {
			Timer.StartTimer (5);
			stratButton.SetActive (false);
		}
	}


	public void OnSide1Click()
	{
		side_1Text.IncreaseValue ();
		if (!Timer.isTimerStarted) 
		{
			Timer.StartTimer (5);
		}
	}
}
