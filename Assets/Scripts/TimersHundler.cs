using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersHundler : MonoBehaviour
{

	public GameObject stratButton;

	public void OnSide0Click ()
	{
		
		if (Timer.isTimerStarted) {
			LocalPresidentText.IncreaseValue ();
			GoogleAutho.Message ();
		} else {			
			stratButton.SetActive (true);
		}  
        
	}



	public void StartGame ()
	{
		if (!Timer.isTimerStarted) {
			Timer.StartTimer (5);
			stratButton.SetActive (false);
		}
	}


	public void OnSide1Click ()
	{
		EnemyPresidentText.IncreaseValue ();
		if (!Timer.isTimerStarted) {
			Timer.StartTimer (5);
		}
	}


}
