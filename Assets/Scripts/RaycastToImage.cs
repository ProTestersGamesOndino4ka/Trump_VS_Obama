﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastToImage : MonoBehaviour
{

	private RaycastHit2D _hit;
	public GameObject _objectCastingRay;
	private Image _presidentImageHittedByRay;

	public void MakeRay ()
	{
		_hit = Physics2D.Raycast (_objectCastingRay.transform.position, -Vector2.up);
		_presidentImageHittedByRay = _hit.transform.gameObject.GetComponent<Image> ();
		Debug.Log (_presidentImageHittedByRay.sprite.ToString ());
		FlagHundler.SetFlagSprite (_presidentImageHittedByRay.sprite.name);
		LocalPresidentImage.SetCurrentPresidentImage (_presidentImageHittedByRay);
	}

	//По сути метод в данный момент бесполезен,
	//так как нигде не используется,
	//но по нему можно в любой момент получить Image президента
	public Image GetImageHittedByRay ()
	{
		_hit = Physics2D.Raycast (_objectCastingRay.transform.position, -Vector2.up);
		_presidentImageHittedByRay = _hit.transform.gameObject.GetComponent<Image> ();
		return _presidentImageHittedByRay;
	}

}
