using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimScript : MonoBehaviour {

	public Animator anim;

	public void NextImageAnimation()
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
		default:
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
	}
	public void PrevImageAnimation()
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
		default:
			break;
		}
		Debug.Log ("Position " + anim.GetInteger ("position"));
	}
}
