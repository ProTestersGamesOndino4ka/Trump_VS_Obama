using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestRayCast : MonoBehaviour {

	RaycastHit2D _hit;
	Ray _ray;

	public void MakeRay()
	{
		//_ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 3, 0));
		_hit = Physics2D.Raycast(transform.position, -Vector2.up);
		Image _image = _hit.transform.gameObject.GetComponent<Image>();
		Debug.Log (_image.sprite.ToString ());
	}

}
