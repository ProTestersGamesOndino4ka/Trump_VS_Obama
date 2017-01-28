using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class side_0Timer : MonoBehaviour {

	private static Text side0_timer;

	void Start()
	{
		side0_timer = GetComponent<Text> ();
	}
	public static void SetValue(float value)
	{
		if (value < 0) {
			side0_timer.text = "0";
		} else {
			side0_timer.text = value.ToString ();
		}
	}
}
