using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPresidentText : MonoBehaviour {

	private static Text _localPresident;
	public static int scoreValue;

	void Start()
	{
		_localPresident = GetComponent<Text> ();
		scoreValue = 0;
		_localPresident.text = scoreValue.ToString();
	}
	public static void Refresh()
	{
		scoreValue = 0;
		SetValue ();
	}
	public static void SetValue()
	{
		_localPresident.text = scoreValue.ToString();
	}

	public static void IncreaseValue()
	{
		_localPresident.text = (++scoreValue).ToString ();
	}

}
