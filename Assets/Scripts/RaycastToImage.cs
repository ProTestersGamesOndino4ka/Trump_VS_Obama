using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastToImage : MonoBehaviour
{

	private RaycastHit2D _hit;
	public GameObject _objectCastingRay;

	public static Image _presidentImageHittedByRay { get; private set; }


	public void MakeRay()
	{		
		if(GetImageHittedByRay() != null)
		{
			ChangePresidentAnimationHandler.isPlayingAnimation = false;
			Debug.Log(_presidentImageHittedByRay.sprite.ToString());
			LocalPresident.SetCurrentPresidentImage(_presidentImageHittedByRay);
			if(GameObject.FindGameObjectWithTag("FlagImage") != null)
			{
				FlagHandler.SetFlagSprite(_presidentImageHittedByRay.sprite.name);
			}
			else
			{
				Debug.LogWarning("FlagImage doesn't exists!");
			}
		}
	}

	public Image GetImageHittedByRay()
	{
		_objectCastingRay = GameObject.FindGameObjectWithTag("RayObject");
		if(_objectCastingRay != null)
		{
			_hit = Physics2D.Raycast(_objectCastingRay.transform.position, -Vector2.up);
			_presidentImageHittedByRay = _hit.transform.gameObject.GetComponent<Image>();
			return _presidentImageHittedByRay;
		}
		else
		{
			return null;
		}
	}

}
