using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastToImage : MonoBehaviour
{

	private RaycastHit2D _hit;
	private GameObject _objectCastingRay;
	private Image _presidentImageHittedByRay;

	public void MakeRay ()
	{		
		if (GetImageHittedByRay () != null) {
			Debug.Log (_presidentImageHittedByRay.sprite.ToString ());
			FlagHandler.SetFlagSprite (_presidentImageHittedByRay.sprite.name);
			LocalPresidentImage.SetCurrentPresidentImage (_presidentImageHittedByRay);
		}
	}

	public Image GetImageHittedByRay ()
	{
		_objectCastingRay = GameObject.FindGameObjectWithTag ("RayObject");
		if (_objectCastingRay != null) {
			_hit = Physics2D.Raycast (_objectCastingRay.transform.position, -Vector2.up);
			_presidentImageHittedByRay = _hit.transform.gameObject.GetComponent<Image> ();
			return _presidentImageHittedByRay;
		} else {
			return null;
		}
	}

}
