using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class DataParser
{

	private static readonly string FILE_NAME = "JSON_DATA.txt";
	private static readonly int INDEX_OF_PRESIDENT_IDS_STRING = 1;
	private static readonly int TARGET_DATASUBSTRINGS_LENGTH = 4;
	private static readonly int INDEX_OF_MAXSCORE_STRING = 3;
	private static readonly string SAMPLE_DATA_STRING = "\"localPresidents\":1;2;3,\"recordScore\":0";

	private static string _dataString;
	private static string _pathToFile;
	private static List<int> localPresidentIDs;
	private static int maxScore;

	public DataParser ()
	{
		#if UNITY_EDITOR
		if (string.IsNullOrEmpty (_pathToFile)) {
			_pathToFile = Application.dataPath + "/" + FILE_NAME;
		}
		#else
		if (string.IsNullOrEmpty(_pathToFile)) {
		    _pathToFile = Application.persistentDataPath + "/" + FILE_NAME;
		}
		#endif
	}

	public void ReadDataFromFile ()
	{		
		if (!File.Exists (_pathToFile)) {
			MakeDataFile ();
		} else {			
			if (!ReadData ()) {
				MakeDataFile ();
			}
		}

		ParseDataStringToElements ();
	}

	private void MakeDataFile ()
	{
		if (!string.IsNullOrEmpty (GooglePlayGames_CloudSystem.GetLoadedDataString ())) {
			_dataString = GooglePlayGames_CloudSystem.GetLoadedDataString ();
		} else {
			var fileWriter = File.CreateText (_pathToFile);
			fileWriter.Write (SAMPLE_DATA_STRING);
			fileWriter.Close ();
			ReadData ();
		}
	}

	private bool ReadData ()
	{
		var fileReader = File.OpenText (_pathToFile);
		_dataString = fileReader.ReadToEnd ();       
		fileReader.Close ();
		return !string.IsNullOrEmpty (_dataString);
	}

	private void ParseDataStringToElements ()
	{
		GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n_data: " + _dataString;
		if (!string.IsNullOrEmpty (_dataString)) {
			string[] dataSubstrings = new string[]{ };
			try {
				dataSubstrings = _dataString.Split (',', ':');
			} catch (IndexOutOfRangeException) {
				Debug.LogWarning ("Error on Split _dataString");
				DeleteFile ();
				return;
			}
			try {
				localPresidentIDs = dataSubstrings [INDEX_OF_PRESIDENT_IDS_STRING].Split (';').Select (s => int.Parse (s)).ToList<int> ();
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n IDS: " + string.Join (";", localPresidentIDs.Select (x => x.ToString ()).ToArray<string> ());
				LocalRecords.SetMyPresidents ();
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "Set local presidents";
				LocalPresidentImage.SetCurrentPresidentImage (LocalPresidentImage.GetCurrentPresidentImage ());
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "Set price";
			} catch (FormatException) {
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n Error on parse PresidetsID ";
				Debug.LogWarning ("Error on parse PresidetsID");
				DeleteFile ();
				return;
			} catch (IndexOutOfRangeException) {
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n Error on Split substring or wrong INDEX_OF_PRESIDENT_IDS_STRING ";
				Debug.LogWarning ("Error on Split substring or wrong INDEX_OF_PRESIDENT_IDS_STRING");
				DeleteFile ();
				return;
			}

			if (dataSubstrings.Length == TARGET_DATASUBSTRINGS_LENGTH) {
				try {
					GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\n" + dataSubstrings.Length;
					maxScore = int.Parse (dataSubstrings [INDEX_OF_MAXSCORE_STRING].Replace ('}', '\0'));
				} catch (FormatException) {
					Debug.LogWarning ("Error on parse \"localrecord\"");
					DeleteFile ();
					return;
				} catch (IndexOutOfRangeException) {
					Debug.LogWarning ("Wrong index of dataSubstrings");
				}
			} else {
				Debug.LogWarning ("Wrong substring length");
				GameObject.FindGameObjectWithTag ("DebugText").GetComponent<Text> ().text += "\nWrong substring length= " + dataSubstrings.Length;
				DeleteFile ();
				return;
			}

		} else {
			Debug.LogWarning ("DataString is Null Or Empty");
			DeleteFile ();
			return;
		}
	}

	public string GetLocalDataString ()
	{		
		return CreateLocalDataString ();
	}

	private string CreateLocalDataString ()
	{
		string tempDataString = string.Empty;
		tempDataString += "\"localPresidents\":";
		tempDataString += string.Join (";", localPresidentIDs.Select (x => x.ToString ()).ToArray<string> ());
		tempDataString += ",\"maxScore\":";
		tempDataString += maxScore.ToString ();
		_dataString = tempDataString;
		return _dataString;
	}


	public static List<int> GetLocalPresidentIDs ()
	{
		return localPresidentIDs;
	}

	public int GetMaxScore ()
	{
		return maxScore; 
	}

	public static void AddPresidentID (int boughtPresidentID)
	{
		if (localPresidentIDs != null) {
			if (localPresidentIDs.Contains (boughtPresidentID)) {
				Debug.LogWarning ("This president already bought!");
				return;
			} else if (LocalRecords.allPresidents != null) {
				if (LocalRecords.allPresidents.Exists (x => x.ID == boughtPresidentID)) {
					localPresidentIDs.Add (boughtPresidentID);
					Debug.Log ("President ID added to list");
				} else {
					Debug.LogWarning ("President Id not found!");
				}
			}
		}
	}

	public static void SetMaxScore (string score)
	{
		int tempScore = int.Parse (score);
		if (tempScore > maxScore) {
			maxScore = tempScore;
		}
	}

	public void SaveDataStringInFile ()
	{
		var fileWritter = File.CreateText (_pathToFile);
		fileWritter.WriteLine (_dataString);
		fileWritter.Close ();
	}

	public void SetDataStringFromLoadedString (string loadedDataString)
	{
		_dataString = loadedDataString;
		this.ParseDataStringToElements ();
	}

	private void DeleteFile ()
	{
		File.Delete (_pathToFile);
		GooglePlayGames_CloudSystem cloud = new GooglePlayGames_CloudSystem ();
	}
}
