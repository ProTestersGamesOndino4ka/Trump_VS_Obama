using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePresidentAnimationHundler : MonoBehaviour
{

	public Animator anim;

	public void SetPosition (int _pos)
	{
		anim.SetInteger ("position", _pos);
	}

	public void NextImageAnimation ()
	{
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
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
	}

	public void PrevImageAnimation ()
	{
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
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
	}
}
