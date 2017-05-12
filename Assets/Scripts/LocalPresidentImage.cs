using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPresidentImage : MonoBehaviour
{
	public static bool isReady{ get; private set; }

	public Animator start_button_anim;
	private static Image _currentPresidentImage;
	private static bool isFree = false;
	private static readonly string NO_PRICE_TEXT = "START";

	void Start ()
	{
		isReady = false;
	}

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
				GameObject.FindGameObjectWithTag ("StartBuyButton").GetComponentInChildren<Text> ().text = NO_PRICE_TEXT;
				isFree = true;
			} else {
				GameObject.FindGameObjectWithTag ("StartBuyButton").GetComponentInChildren<Text> ().text = LocalRecords.allPresidents.Find (x => x.ImageName == _currentPresidentImage.sprite.name).Price;
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
		if (!ChangePresidentAnimationHandler.isPlayingAnimation) {
			if (isFree) {
				Debug.Log ("Playing");
				ScorePanelAnimationHandler.SetStart (GameObject.Find ("Panel_local_score").GetComponent<Animator> (), true);
				SetButtonsActiveState (false);
				if (!isReady) {
					isReady = true;
				}
			} else {
				Payments.Buy (LocalRecords.allPresidents.Find (x => x.ImageName == _currentPresidentImage.sprite.name).ID);
			}
		}
	}

	public void SetButtonsActiveState (bool value)
	{
		GameObject.Find ("Prev_local").GetComponent<Animator> ().SetBool ("isActive", value);
		GameObject.Find ("Next_local").GetComponent<Animator> ().SetBool ("isActive", value);
		if (!value) {
			start_button_anim.Play ("Disabled");
			start_button_anim.Play ("fall_down");
		} else {
			start_button_anim.Play ("fall_up");
			start_button_anim.Play ("Normal");
		}
	}

	public void onTap ()
	{
		if (EnemyPresidentImage.isReady) {
			if (!Timer.isTimerStarted) {
				Timer.StartTimer (10);
				new TimersHandler ().OnSide0Click ();
			} else {
				new TimersHandler ().OnSide0Click ();	
			}
		}
	}


}
