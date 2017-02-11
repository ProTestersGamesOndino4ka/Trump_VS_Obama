using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameUI : MonoBehaviour
{


	void Start ()
	{
		
	}

	public void OpenSelectSavedGamesUI ()
	{
		GooglePlayGames_CloudSystem.ShowSelectUI ();
	}
}
