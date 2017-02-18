using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresidentImage : MonoBehaviour
{
	private static Image _currentPresidentImage;
	private static bool isFree = false;
	private static readonly string NO_PRICE_TEXT = "START";

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
		if (_currentPresidentImage != null) {
			if (LocalRecords.myPresidents.Exists (x => x.ImageName == _currentPresidentImage.sprite.name)) {
				GameObject.FindGameObjectWithTag ("StartButton_enemy").GetComponentInChildren<Text> ().text = NO_PRICE_TEXT;
				isFree = true;
			} else {
				GameObject.FindGameObjectWithTag ("StartButton_enemy").GetComponentInChildren<Text> ().text = LocalRecords.allPresidents.Find (x => x.ImageName == _currentPresidentImage.sprite.name).Price;
				isFree = false;
			}
		} else {
			Image tempPresidentImage = new RaycastToImage ().GetImageHittedByRay ();	
			if (tempPresidentImage != null) {
				SetCurrentPresidentImage (tempPresidentImage);
			}
		}
	}

	public void OnStartBuyButtonClick ()
	{
		if (!ChangePresidentAnimationHandler.isPlayingAnimation && isFree) {
			Debug.Log ("Playing");
		} 

	}

}
