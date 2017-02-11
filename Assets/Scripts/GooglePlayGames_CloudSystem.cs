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
		DataParser _data = new DataParser ();
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
		GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nLoadedDataString: " + loadedDataString;
	}

	public static bool isAuthenticated {
		get{ return Social.Active.localUser.authenticated; }
	}

	public void SaveDataToCloud (DataParser data)
	{
		if (isAuthenticated) {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text = "Authenticated To save";
			if (loadedDataString != data.GetLocalDataString ()) {
				_saveDataString = data.GetLocalDataString ();
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nstring to save: " + _saveDataString;
				data.SaveDataStringInFile ();
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nSaved in file ";
				try {
					((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution (
						SAVE_NAME,
						DataSource.ReadCacheOrNetwork,
						ConflictResolutionStrategy.UseLongestPlaytime,
						SaveData
					);
				} catch (System.Exception ex) {
					GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n" + ex.Message;
				}

			}
		}
	}

	private void SaveData (SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success && !string.IsNullOrEmpty (_saveDataString)) {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nGood status";
			byte[] savingData = ToBytes (_saveDataString);
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nbytes ";
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder ();
			SavedGameMetadataUpdate updateMetadata = builder.Build ();
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nBuilders";
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
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nGame written";
			Debug.Log ("Game " + game.Description + "written");
			DataParser data = new DataParser ();
			data.SetDataStringFromLoadedString (_saveDataString);
		} else {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nGame NOT written";
			Debug.LogWarning ("Error saving game: " + status);
		}
	}

	public void LoadDataFromCloud ()
	{		
		GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nTry Load from cloud";
		if (isAuthenticated) {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nAuth";
			Debug.Log ("Loading data from cloud");
			try {

				((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution (
					SAVE_NAME,
					DataSource.ReadCacheOrNetwork,
					ConflictResolutionStrategy.UseLongestPlaytime,
					LoadData
				);	
			} catch (System.Exception ex) {
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n" + ex.Message;
			}
		} else {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nNot authenticated";
			Debug.Log ("Not authenticated");
		}
	}

	private void LoadData (SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\ntry load data";
		((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData (
			game,
			SavedDataLoaded
		);
	}

	private void SavedDataLoaded (SavedGameRequestStatus status, byte[] data)
	{
		if (status == SavedGameRequestStatus.Success) {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nData load seccess";
			Debug.Log ("Data load seccess");
			if (data != null) {
				loadedDataString = FromBytes (data);
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nSaved loaded data to loadedDataString" + loadedDataString;
				DataParser dataString = new DataParser ();
				dataString.SetDataStringFromLoadedString (loadedDataString);
				Debug.Log ("Saved loaded data to loadedDataString" + loadedDataString);				
			} else {				
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nLoading data is NULL!";
				Debug.LogWarning ("Loading data is NULL!");
			}
		} else {
			GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nError loading data from cloud!";
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
}
