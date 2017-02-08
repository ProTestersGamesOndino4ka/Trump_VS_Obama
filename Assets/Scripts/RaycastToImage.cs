using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaycastToImage : MonoBehaviour {

	private RaycastHit2D _hit;
	public GameObject _objectCastingRay;

	public void MakeRay()
	{
		_hit = Physics2D.Raycast(_objectCastingRay.transform.position, -Vector2.up);
		Image _image = _hit.transform.gameObject.GetComponent<Image>();
		Debug.Log (_image.sprite.ToString ());
		FlagChanger.SetFlagSprite (_image.sprite.name);
	}

}
