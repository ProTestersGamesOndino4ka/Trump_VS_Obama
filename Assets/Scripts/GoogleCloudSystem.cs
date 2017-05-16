using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GoogleCloudSystem
{

	private static readonly string SAVE_NAME = "game_save";

	public void Initialize()
	{
		if(!isAuthenticated)
		{			
			try
			{
				LoadDataFromCloud();
				SaveDataManager.ReadDataFromCloud();
			}
			catch (System.Exception)
			{
				SaveDataManager.ReadDataFromFile(true);
			}
		}
		else
		{
			SaveDataManager.ReadDataFromFile(true);
		}
	}

	public static bool isAuthenticated {
		get{ return Social.Active.localUser.authenticated; }
	}

	public void SaveUserDataToCloud()
	{	
		try
		{
			((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
				SAVE_NAME,
				DataSource.ReadCacheOrNetwork,
				ConflictResolutionStrategy.UseLongestPlaytime,
				SaveData
			);
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning("Error on SaveUserDataToCloud; " + ex.Message);
		}
	}

	private void SaveData(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if(status == SavedGameRequestStatus.Success)
		{
			Debug.Log("Good status");
			byte[] savingData = SaveDataManager.GetBytesFromDataString();
			Debug.Log("bytes");
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
			SavedGameMetadataUpdate updateMetadata = builder.Build();
			Debug.Log("Builders");
			((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(
				game,
				updateMetadata,
				savingData,
				SavedDataWritten
			);
		}
	}

	private void SavedDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if(status == SavedGameRequestStatus.Success)
		{
			Debug.Log("Game< " + game.Description + " >written");
		}
		else
		{
			Debug.LogWarning("Error saving game: " + status);
		}
	}

	public void LoadDataFromCloud()
	{		
		Debug.Log("Try Load from cloud");
		if(isAuthenticated)
		{
			Debug.Log("Auth");
			Debug.Log("Loading data from cloud");
			try
			{

				((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
					SAVE_NAME,
					DataSource.ReadCacheOrNetwork,
					ConflictResolutionStrategy.UseLongestPlaytime,
					LoadData
				);	
			}
			catch (System.Exception ex)
			{
				Debug.Log("" + ex.Message);
			}
		}
		else
		{
			Debug.Log("Not authenticated");
		}
	}

	private void LoadData(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		Debug.Log("try load data");
		((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(
			game,
			SavedDataLoaded
		);
	}

	private void SavedDataLoaded(SavedGameRequestStatus status, byte[] data)
	{
		if(status == SavedGameRequestStatus.Success)
		{
			Debug.Log("Data load seccess");
			if(data != null)
			{
				SaveDataManager.SetCloudData(data);		
			}
			else
			{
				Debug.LogWarning("Loading data is NULL!");
			}
		}
		else
		{
			Debug.LogWarning("Error loading data from cloud!");
		}
	}





	public static void ShowSelectUI()
	{
		uint maxNumToDisplay = 5;
		bool allowCreateNew = true;
		bool allowDelete = true;

		ISavedGameClient saveGameClient = PlayGamesPlatform.Instance.SavedGame;
		saveGameClient.ShowSelectSavedGameUI(
			"Saved Games",
			maxNumToDisplay,
			allowCreateNew,
			allowDelete,
			OnSavedGameSelected
		);
	}

	static void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
	{
		if(status == SelectUIStatus.SavedGameSelected)
		{
			Debug.Log("selected game" + game.Description);
		}
		else
		{
			Debug.Log("Error by selected game");
		}
	}
}
