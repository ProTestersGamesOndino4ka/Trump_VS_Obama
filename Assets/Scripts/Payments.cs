using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payments
{

	public Payments ()
	{
		//Some Google Play shit
	}

	public static void Buy (int presidentID)
	{
		//Some Google Play shit
		DataParser.AddPresidentID (presidentID);
		GooglePlayGames_CloudSystem cloudSave = new GooglePlayGames_CloudSystem ();
		cloudSave.SaveDataToCloud (new DataParser ());
	}

}
