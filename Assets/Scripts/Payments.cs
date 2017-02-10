using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payments
{

	public Payments ()
	{
		//Some Google Play shit
		//	_cloud = new GooglePlayGames_CloudSystem ();

	}

	public void Buy (int presidentID)
	{
		//Some Google Play shit
		DataParser.AddPresidentID (presidentID);
		//	_cloud.SaveDataToCloud (_data);
	}

}
