using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GooglePlayGames_CloudSystem
{

	private const string SAVE_NAME = "game_save";

	private string _saveDataString;
	private static string loadedDataString;

	public void Initialize ()
	{
		SaveDataManager _data = new SaveDataManager ();
		try {
			LoadDataFromCloud ();
		} catch (System.Exception ex) {
			Debug.LogWarning ("Unhandled exception " + ex.Message);
			if (string.IsNullOrEmpty (loadedDataString)) {			
				_data.ReadDataFromFile ();
			} else {
				_data.SetDataStringFromLoadedString (loadedDataString);
			}
		}
		if (!isAuthenticated) {
			_data.ReadDataFromFile ();
		}
	}

	public static bool isAuthenticated {
		get{ return Social.Active.localUser.authenticated; }
	}

	public void SaveDataToCloud (SaveDataManager data)
	{
		if (isAuthenticated) {
			if (loadedDataString != data.GetLocalDataString ()) {
				_saveDataString = data.GetLocalDataString ();
				data.SaveDataStringInFile ();
				try {
					((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution (
						SAVE_NAME,
						DataSource.ReadCacheOrNetwork,
						ConflictResolutionStrategy.UseLongestPlaytime,
						SaveData
					);
				} catch (System.Exception ex) {
					Debug.LogWarning ("Same loaded string as file" + ex.Message);
				}

			}
		}
	}

	private void SaveData (SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success && !string.IsNullOrEmpty (_saveDataString)) {
			Debug.Log ("Good status");
			byte[] savingData = ToBytes (_saveDataString);
			Debug.Log ("bytes ");
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder ();
			SavedGameMetadataUpdate updateMetadata = builder.Build ();
			Debug.Log ("Builders");
			((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate (
				game,
				updateMetadata,
				savingData,
				SavedDataWritten
			);
		}
	}

	private void SavedDataWritten (SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success) {
			Debug.Log ("Game written");
			Debug.Log ("Game " + game.Description + "written");
			SaveDataManager data = new SaveDataManager ();
			data.SetDataStringFromLoadedString (_saveDataString);
		} else {
			Debug.Log ("Game NOT written");
			Debug.LogWarning ("Error saving game: " + status);
		}
	}

	public void LoadDataFromCloud ()
	{		
		Debug.Log ("Try Load from cloud");
		if (isAuthenticated) {
			Debug.Log ("Auth");
			Debug.Log ("Loading data from cloud");
			try {

				((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution (
					SAVE_NAME,
					DataSource.ReadCacheOrNetwork,
					ConflictResolutionStrategy.UseLongestPlaytime,
					LoadData
				);	
			} catch (System.Exception ex) {
				Debug.Log ("" + ex.Message);
			}
		} else {
			Debug.Log ("Not authenticated");
		}
	}

	private void LoadData (SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		Debug.Log ("try load data");
		((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData (
			game,
			SavedDataLoaded
		);
	}

	private void SavedDataLoaded (SavedGameRequestStatus status, byte[] data)
	{
		if (status == SavedGameRequestStatus.Success) {
			Debug.Log ("Data load seccess");
			if (data != null) {
				loadedDataString = FromBytes (data);
				Debug.Log ("Saved loaded data to loadedDataString = " + loadedDataString);
				SaveDataManager dataString = new SaveDataManager ();
				dataString.SetDataStringFromLoadedString (loadedDataString);
				Debug.Log ("Saved loaded data to loadedDataString" + loadedDataString);				
			} else {
				Debug.LogWarning ("Loading data is NULL!");
			}
		} else {
			Debug.LogWarning ("Error loading data from cloud!");
		}
	}

	public static string GetLoadedDataString ()
	{
		return loadedDataString;
	}

	private byte[] ToBytes (string message)
	{
		byte[] bytes = Encoding.UTF8.GetBytes (message);
		return bytes;
	}

	private string FromBytes (byte[] bytes)
	{
		string saveString = Encoding.UTF8.GetString (bytes);
		return saveString;
	}


	public static void ShowSelectUI ()
	{
		uint maxNumToDisplay = 5;
		bool allowCreateNew = true;
		bool allowDelete = true;

		ISavedGameClient saveGameClient = PlayGamesPlatform.Instance.SavedGame;
		saveGameClient.ShowSelectSavedGameUI (
			"Saved Games",
			maxNumToDisplay,
			allowCreateNew,
			allowDelete,
			OnSavedGameSelected
		);
	}

	static void OnSavedGameSelected (SelectUIStatus status, ISavedGameMetadata game)
	{
		if (status == SelectUIStatus.SavedGameSelected) {
			Debug.Log ("selected game" + game.Description);
		} else {
			Debug.Log ("Error by selected game");
		}
	}
}
