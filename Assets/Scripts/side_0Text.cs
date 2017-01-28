using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class side_0Text : MonoBehaviour {

	private static Text side0;
	public static int scoreValue;

	void Start()
	{
		side0 = GetComponent<Text> ();
		scoreValue = 0;
		side0.text = scoreValue.ToString();
	}
	public static void Refresh()
	{
		scoreValue = 0;
		SetValue ();
	}
	public static void SetValue()
	{
		side0.text = scoreValue.ToString();
	}

	public static void IncreaseValue()
	{
		side0.text = (++scoreValue).ToString ();
	}

}
