using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class side_1Text : MonoBehaviour {

	static Text side1;
	public static  int scoreValue;

	void Start()
	{
		side1 = GetComponent<Text> ();
		scoreValue = 0;
		side1.text = scoreValue.ToString();
	}
	public static void Refresh()
	{
		scoreValue = 0;
		SetValue ();
	}
    public static void SetValue()
    {
        side1.text = scoreValue.ToString();
    }

    public static void IncreaseValue()
	{
		side1.text = (++scoreValue).ToString ();
	}
}
