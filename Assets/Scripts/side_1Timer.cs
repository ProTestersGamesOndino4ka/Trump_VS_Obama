using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class side_1Timer : MonoBehaviour {

	private static Text side1_timer;

	void Start()
	{
		side1_timer = GetComponent<Text> ();
	}
	public static void SetValue(float value)
	{
		if (value < 0) {
			side1_timer.text = "0";
		} else {
			side1_timer.text = value.ToString ();
		}
	}
}
