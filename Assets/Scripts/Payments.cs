using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payments
{

	public Payments()
	{
		//Some Google Play shit
	}

	public static void Buy(string presidentID)
	{
		//Some Google Play shit
		SaveDataManager.AddPresidentID(presidentID);
		SaveDataManager.SaveUserData();
		SaveDataManager.ReadDataFromFile(false);
		//LoadScene.LoadGamemodeScene();
		LocalPresidentImage.SetCurrentPresidentImage(LocalPresidentImage.GetCurrentPresidentImage());
		EnemyPresidentImage.SetCurrentPresidentImage(EnemyPresidentImage.GetCurrentPresidentImage());
	}

}
