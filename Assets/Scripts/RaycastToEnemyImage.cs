using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastToEnemyImage : MonoBehaviour
{
	private RaycastHit2D _hit;
	public GameObject _objectCastingRay;
	public Animator animButton;
	public string upAnimationClipName;

	public static Image _presidentImageHittedByRay { get; private set; }


	public void MakeRay ()
	{		
		if (GetImageHittedByRay () != null) {
			ChangePresidentAnimationHandler.isPlayingAnimation = false;
			animButton.Play (upAnimationClipName);
			Debug.Log (_presidentImageHittedByRay.sprite.ToString ());
			EnemyPresidentImage.SetCurrentPresidentImage (_presidentImageHittedByRay);
		}
	}

	public Image GetImageHittedByRay ()
	{
		if (_objectCastingRay == null) {
			_objectCastingRay = GameObject.FindGameObjectWithTag ("RayObject_enemy");
		}
		if (_objectCastingRay != null) {
			_hit = Physics2D.Raycast (_objectCastingRay.transform.position, -Vector2.up);
			_presidentImageHittedByRay = _hit.transform.gameObject.GetComponent<Image> ();
			return _presidentImageHittedByRay;
		} else {
			Debug.LogWarning ("LOGGER_RayObject_enemy_is_null");
			return null;

		}
	}
}
