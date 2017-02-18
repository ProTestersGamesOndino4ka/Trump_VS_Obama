using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePresidentAnimationHandler : MonoBehaviour
{

	public Animator anim;
	public Animator animButton;
	public static bool isPlayingAnimation;
	public string downAnimationClipName;

	public void SetPosition (int _pos)
	{
		anim.SetInteger ("position", _pos);
	}

	public void NextImageAnimation ()
	{
		isPlayingAnimation = true;
		switch (anim.GetInteger ("position")) {
		case 0:
			anim.SetInteger ("position", 1);
			break;
		case 1:
			anim.SetInteger ("position", 2);
			break;
		case 2:
			anim.SetInteger ("position", 3);
			break;
		case 3:
			anim.SetInteger ("position", 4);
			break;
		case 4:
			anim.SetInteger ("position", 5);
			break;
		case 5:
			anim.SetInteger ("position", 6);
			break;
		case 6:
			anim.SetInteger ("position", 7);
			break;
		case 7:
			anim.SetInteger ("position", 8);
			break;
		case 8:
			anim.SetInteger ("position", 9);
			break;
		default:
			isPlayingAnimation = false;
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
		if (isPlayingAnimation) {
			animButton.Play (downAnimationClipName);
		}

	}

	public void PrevImageAnimation ()
	{
		isPlayingAnimation = true;
		switch (anim.GetInteger ("position")) {
		case 1:
			anim.SetInteger ("position", 0);
			break;
		case 2:
			anim.SetInteger ("position", 1);
			break;
		case 3:
			anim.SetInteger ("position", 2);
			break;
		case 4:
			anim.SetInteger ("position", 3);
			break;
		case 5:
			anim.SetInteger ("position", 4);
			break;
		case 6:
			anim.SetInteger ("position", 5);
			break;
		case 7:
			anim.SetInteger ("position", 6);
			break;
		case 8:
			anim.SetInteger ("position", 7);
			break;
		case 9:
			anim.SetInteger ("position", 8);
			break;
		default:
			isPlayingAnimation = false;
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
		if (isPlayingAnimation) {
			animButton.Play (downAnimationClipName);
		}
	}
}
