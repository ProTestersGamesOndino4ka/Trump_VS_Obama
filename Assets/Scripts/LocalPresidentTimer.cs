using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPresidentTimer : MonoBehaviour {

	private static Text _localPresidentTimer;

	void Start()
	{
		_localPresidentTimer = GetComponent<Text> ();
	}
	public static void SetValue(float value)
	{
		if (value < 0) {
			_localPresidentTimer.text = "0";
		} else {
			_localPresidentTimer.text = value.ToString ();
		}
	}
}
