using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalScoreText : MonoBehaviour
{

	private static Text _localPresident;

	void Start()
	{
		_localPresident = GetComponent<Text>();
	}

	public static void SetText(string scoreText)
	{
		_localPresident.text = scoreText;
	}

}
