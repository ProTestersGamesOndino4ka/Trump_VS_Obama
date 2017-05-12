using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresidentImage : MonoBehaviour
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
			ScorePanelAnimationHandler.SetStart (GameObject.Find ("Panel_enemy_score").GetComponent<Animator> (), true);
			SetButtonsActive (false);
			if (!isReady) {
				isReady = true;
			}
		} 

	}

	private void SetButtonsActive (bool value)
	{
		GameObject.Find ("Prev_enemy").GetComponent<Animator> ().SetBool ("isActive", value);
		GameObject.Find ("Next_enemy").GetComponent<Animator> ().SetBool ("isActive", value);
		if (!value) {
			start_button_anim.Play ("fall_down");
		} else {
			start_button_anim.Play ("fall_up");
		}
	}

	public void onTap ()
	{
		
		if (LocalPresidentImage.isReady) {
			if (!Timer.isTimerStarted) {
				Timer.StartTimer (10);
				new TimersHandler ().OnSide1Click ();
			} else {
				new TimersHandler ().OnSide1Click ();	
			}
		}
	}

}
