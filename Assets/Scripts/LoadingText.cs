using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
	static Text loadingText;

	void Awake ()
	{
		loadingText = GetComponent<Text> ();
	}

	public static void AddText (string textToAdd)
	{
		
		loadingText.text += "\n" + textToAdd;
	}

}
