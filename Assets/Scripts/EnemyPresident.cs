﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresident : MonoBehaviour
{
	public static bool isReady{ get; private set; }

	private static Image _currentPresidentImage;
	private static bool isFree = false;
	private static readonly string NO_PRICE_TEXT = "START";
	private static readonly Color notReadyColor = new Color32(0, 55, 255, 160);
	private static readonly Color readyColor = new Color32(0, 255, 30, 160);
	private static readonly Color loseColor = new Color32(255, 0, 0, 160);

	void Start()
	{
		isReady = false;
	}

	public static void SetCurrentPresidentImage(Image presidentImage)
	{
		_currentPresidentImage = presidentImage;
		SetPriceToButton();
	}

	public static Image GetCurrentPresidentImage()
	{
		return _currentPresidentImage;
	}

	private static void SetPriceToButton()
	{
		if(_currentPresidentImage != null)
		{
			if(LocalRecords.myPresidents.Exists(x => x.ImageName == _currentPresidentImage.sprite.name))
			{
				SetTextToStartButton(NO_PRICE_TEXT);
				isFree = true;

			}
			else
			{
				SetTextToStartButton(LocalRecords.allPresidents.Find(x => x.ImageName == _currentPresidentImage.sprite.name).Price);
				isFree = false;
			}
		}
		else
		{
			Image tempPresidentImage = new RaycastToImage().GetImageHittedByRay();	
			if(tempPresidentImage != null)
			{
				SetCurrentPresidentImage(tempPresidentImage);
			}
		}
	}


	public static void SetTextToStartButton(string text)
	{
		GameObject.FindGameObjectWithTag("StartButton_enemy").GetComponentInChildren<Text>().text = text;
	}

	public void OnStartBuyButtonClick()
	{
		if(!ChangePresidentAnimationHandler.isPlayingAnimation && isFree)
		{
			Debug.Log("Playing");
			SetButtonsActiveState(false);
			if(!isReady)
			{
				isReady = true;
				SetReadyButtonColor();
				if(LocalPresident.isReady)
				{
					CountdownTimer.StartCountdown();
				}
			}
		} 

	}

	private void SetButtonsActiveState(bool value)
	{
		GameObject.Find("Prev_enemy").GetComponent<Animator>().SetBool("isActive", value);
		GameObject.Find("Next_enemy").GetComponent<Animator>().SetBool("isActive", value);
	}

	private void SetReadyButtonColor()
	{
		GameObject.Find("StartBuyButton_enemy").GetComponent<Image>().color = readyColor; 
	}

	private void SetNotReadyButtonColor()
	{
		GameObject.Find("StartBuyButton_enemy").GetComponent<Image>().color = notReadyColor; 
	}

	public void onTap()
	{
		
		if(GameTimer.isTimerStarted)
		{
			ScoreHandler.IncreaseEnemyScore();
		}
	}

	public static void onLose()
	{
		GameObject.Find("StartBuyButton_enemy").GetComponent<Image>().color = loseColor; 
		SetTextToStartButton("LOSE");
	}

	public static void onWin()
	{
		SetTextToStartButton("WIN");
	}

	public static void onDraw()
	{
		SetTextToStartButton("DRAW");
	}

}
