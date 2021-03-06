﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagHandler : MonoBehaviour
{
	private static Image _flagImage;
	private static List<Sprite> _listOfFlagSprites;
	private static List<Sprite> _listOfPresidentSprites;
	private const float DEFAULT_ASPECT_RATIO = 1.77f;

	void Awake()
	{
		_flagImage = GetComponent<Image>();
		float currentAspectRatio = (float)Screen.height / (float)Screen.width;
		if(currentAspectRatio < DEFAULT_ASPECT_RATIO)
		{
			_flagImage.transform.localPosition = new Vector3(
				_flagImage.transform.localPosition.x,
				_flagImage.transform.localPosition.y - (_flagImage.rectTransform.offsetMax.y - (_flagImage.rectTransform.offsetMax.y * currentAspectRatio / DEFAULT_ASPECT_RATIO)) / 2);
		}
	}

	void Start()
	{
		_listOfFlagSprites = new List<Sprite>(Resources.LoadAll("Flags", typeof(Sprite)).Cast<Sprite>());
		_listOfPresidentSprites = new List<Sprite>(Resources.LoadAll("Images", typeof(Sprite)).Cast<Sprite>());
	}

	public static void SetFlagSprite(string presidentImageName)
	{		
		_flagImage.sprite = _listOfFlagSprites.Find(x => x.name == LocalRecords.allPresidents.Find(y => y.ImageName == presidentImageName).FlagName);
	}

	public static void SetPresidentSprite(string presidentImageName)
	{		
		_flagImage.sprite = _listOfPresidentSprites.Find(x => x.name == LocalRecords.allPresidents.Find(y => y.ImageName == presidentImageName).ImageName);
	}


}
