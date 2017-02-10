using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPresidentImage : MonoBehaviour
{

	private static Image _currentPresidentImage;
	private static bool isFree = false;

	public static void SetCurrentPresidentImage (Image presidentImage)
	{
		_currentPresidentImage = presidentImage;
		SetPriceToButton ();
	}

	public static Image GetCurrentPresidentImage ()
	{
		return _currentPresidentImage;
	}

	private static void SetPriceToButton ()
	{
        
            if (LocalRecords.myPresidents.Exists(x => x.ImageName == _currentPresidentImage.sprite.name))
            {
                GameObject.FindGameObjectWithTag("StartBuyButton").GetComponentInChildren<Text>().text = "START";
                isFree = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("StartBuyButton").GetComponentInChildren<Text>().text = LocalRecords.allPresidents.Find(x => x.ImageName == _currentPresidentImage.sprite.name).Price;
                isFree = false;
            }
        
	}

	public void OnStartBuyButtonClick ()
	{
		if (isFree) {
			Debug.Log ("Playing");
		} else {
			Payments.Buy (LocalRecords.allPresidents.Find (x => x.ImageName == _currentPresidentImage.sprite.name).ID);
		}
	}


}
