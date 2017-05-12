using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelAnimationHandler
{


	public static void SetStart (Animator anim, bool value)
	{
		anim.SetBool ("Start", value);
	}

	public static void SetEnd (Animator anim, bool value)
	{
		anim.SetBool ("End", value);
	}
	

}
