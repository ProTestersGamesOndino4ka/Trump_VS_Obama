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
		SaveDataManager.AddPresidentID (presidentID);
		GooglePlayGames_CloudSystem cloudSave = new GooglePlayGames_CloudSystem ();
		cloudSave.SaveDataToCloud (new SaveDataManager ());
	}

}
