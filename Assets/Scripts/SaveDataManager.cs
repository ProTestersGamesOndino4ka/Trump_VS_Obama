using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

public static class SaveDataManager
{

	private static readonly string FILE_NAME = "JSON_DATA.txt";
	private static readonly int INDEX_OF_PRESIDENT_IDS_STRING = 1;
	private static readonly int TARGET_DATASUBSTRINGS_LENGTH = 4;
	private static readonly int INDEX_OF_MAXSCORE_STRING = 3;
	private static readonly string ENCRYPTED_DEFAULT_USER_DATA = "34;108;111;99;97;108;80;114;101;115;105;100;101;110;116;115;34;58;50;54;53;53;69;54;50;59;49;53;50;52;83;68;54;44;34;114;101;99;111;114;100;83;99;111;114;101;34;58;48";

	private static byte[] cloudData;
	public static string _dataString;
	private static string _pathToFile;
	private static List<string> localPresidentIDs;
	private static int clickPoints;

	static SaveDataManager()
	{
#if UNITY_EDITOR
		if(string.IsNullOrEmpty(_pathToFile))
		{
			_pathToFile = Application.dataPath + "/" + FILE_NAME;
		}
#else
		if (string.IsNullOrEmpty(_pathToFile)) {
		    _pathToFile = Application.persistentDataPath + "/" + FILE_NAME;
		}
#endif
	}

	public static void SetCloudData(byte[] data)
	{
		cloudData = data;
	}

	public static void OnChooseDataSource(bool isCloudData)
	{
		if(isCloudData)
		{
			SaveDataStringInFile(BytesArrayAsString(cloudData));
		}
		ReadDataFromFile(true);
	}

	public static void ReadDataFromCloud()
	{
		if(!File.Exists(_pathToFile))
		{
			if(cloudData != null)
			{
				if(string.IsNullOrEmpty(GetDataStringFromFile()))
				{
					SaveDataStringInFile(BytesArrayAsString(cloudData));
				}
				else
				{
					//Enable buttons to choose
					LoadingText.EnableButtons();
				}
			}
			else
			{
				ReadDataFromFile(true);
			}
		}
		else
		{
			ReadDataFromFile(true);
		}

	}

	public static void ReadDataFromFile(bool isNeedToGoToGamemodeScene)
	{
		if(!File.Exists(_pathToFile))
		{
			RecreateDataFileAndSetDefaultData();
		}
		else
		{
			if(string.IsNullOrEmpty(GetDataStringFromFile()))
			{
				SetDefaultData();
				_dataString = GetDataStringFromFile();
			}
			else
			{
				_dataString = DecryptBytesToString(StringToBytesArray(_dataString));
				//mark to delete
				LoadingText.AddText("Decrypted string: " + _dataString);
			}
		}

		ParseDataStringToElements(isNeedToGoToGamemodeScene);
	}

	private static void SetDefaultData()
	{
		var fileWriter = File.CreateText(_pathToFile);
		fileWriter.Write(ENCRYPTED_DEFAULT_USER_DATA);
		fileWriter.Close();
	}

	private static string GetDataStringFromFile()
	{
		var fileReader = File.OpenText(_pathToFile);
		_dataString = fileReader.ReadToEnd();
		fileReader.Close();
		return _dataString;
	}

	private static void ParseDataStringToElements(bool isNeedToGoToGamemodeScene)
	{
		string[] dataSubstrings = new string[] { };
		try
		{
			dataSubstrings = _dataString.Split(',', ':');
		}
		catch (IndexOutOfRangeException)
		{
			Debug.LogWarning("Error on Split _dataString");
			RecreateDataFileAndSetDefaultData();
			ReadDataFromFile(true);
			return;
		}

		if(dataSubstrings.Length == TARGET_DATASUBSTRINGS_LENGTH)
		{
			try
			{
				localPresidentIDs = dataSubstrings[INDEX_OF_PRESIDENT_IDS_STRING].Split(';').ToList<string>();
				Debug.Log(" IDS: " + string.Join(";", localPresidentIDs.Select(x => x.ToString()).ToArray<string>()));

				//mark to delete
				LoadingText.AddText(" IDS: " + string.Join(";", localPresidentIDs.Select(x => x.ToString()).ToArray<string>()));

				if(!LocalRecords.SetMyPresidents())
				{
					RecreateDataFileAndSetDefaultData();
					ReadDataFromFile(true);
					return;
				}
				Debug.Log("Set local presidents");
			}
			catch (FormatException)
			{
				Debug.LogWarning("Error on parse PresidetsID");
				RecreateDataFileAndSetDefaultData();
				ReadDataFromFile(true);
				return;
			}
			catch (IndexOutOfRangeException)
			{
				Debug.LogWarning("Error on Split substring or wrong INDEX_OF_PRESIDENT_IDS_STRING");
				RecreateDataFileAndSetDefaultData();
				ReadDataFromFile(true);
				return;
			}


			try
			{
				Debug.Log("\nSubstrings count = " + dataSubstrings.Length);
				clickPoints = int.Parse(dataSubstrings[INDEX_OF_MAXSCORE_STRING].Replace('}', '\0'));
				Debug.Log("\nMax score = " + clickPoints);
			}
			catch (FormatException)
			{
				Debug.LogWarning("Error on parse \"localrecord\"");
				RecreateDataFileAndSetDefaultData();
				ReadDataFromFile(true);
				return;
			}
			catch (IndexOutOfRangeException)
			{
				Debug.LogWarning("Wrong index of dataSubstrings");
				RecreateDataFileAndSetDefaultData();
				ReadDataFromFile(true);
				return;
			}
		}
		else
		{
			Debug.LogWarning("Wrong substring length" + dataSubstrings.Length);
			RecreateDataFileAndSetDefaultData();
			ReadDataFromFile(true);
			return;
		}
		if(isNeedToGoToGamemodeScene)
		{
			LoadScene.LoadGamemodeScene();
		}
	}

	public static void SaveUserData()
	{
		//Local Save
		CreateLocalDataString();
		SaveDataStringInFile(BytesArrayAsString(GetBytesFromDataString()));

		//Cloud Save
		new GoogleCloudSystem().SaveUserDataToCloud();
	}

	private static string CreateLocalDataString()
	{
		string tempDataString = string.Empty;
		tempDataString += "\"localPresidents\":";
		tempDataString += string.Join(";", localPresidentIDs.Select(x => x.ToString()).ToArray<string>());
		tempDataString += ",\"maxScore\":";
		tempDataString += clickPoints.ToString();
		_dataString = tempDataString;
		return _dataString;
	}


	public static List<string> GetLocalPresidentIDs()
	{
		return localPresidentIDs;
	}

	public static int GetMaxScore()
	{
		return clickPoints;
	}

	public static void AddPresidentID(string boughtPresidentID)
	{
		if(localPresidentIDs != null)
		{
			if(localPresidentIDs.Contains(boughtPresidentID))
			{
				Debug.LogWarning("This president already bought!");
				return;
			}
			else if(LocalRecords.allPresidents != null)
			{
				if(LocalRecords.allPresidents.Exists(x => x.ID == boughtPresidentID))
				{
					localPresidentIDs.Add(boughtPresidentID);
					Debug.LogWarning("President ID added to list");
				}
				else
				{
					Debug.LogWarning("President Id not found!");
				}
			}
		}
	}

	public static void SetMaxScore(string score)
	{
		int tempScore = int.Parse(score);
		if(tempScore > clickPoints)
		{
			clickPoints = tempScore;
		}
	}

	private static void SaveDataStringInFile(string str)
	{
		var fileWritter = File.CreateText(_pathToFile);
		fileWritter.WriteLine(str);
		fileWritter.Close();
	}

	private static void RecreateDataFile()
	{
		File.Create(_pathToFile).Close();
	}

	private static void RecreateDataFileAndSetDefaultData()
	{
		RecreateDataFile();
		SetDefaultData();
		_dataString = GetDataStringFromFile();
	}

	static public int GetPoints()
	{
		return clickPoints;
	}

	static public void SetPoints(int enterPoints)
	{
		clickPoints += enterPoints;
	}

	public static byte[] GetBytesFromDataString()
	{
		byte[] bytes = Encoding.UTF8.GetBytes(_dataString);
		return bytes;
	}

	public static string DecryptBytesToString(byte[] bytes)
	{
		string saveString = Encoding.UTF8.GetString(bytes);
		return saveString;
	}

	public static string BytesArrayAsString(byte[] bytes)
	{
		string str = string.Join(";", bytes.ToList().Select(x => Convert.ToString(x)).ToArray());
		return str;
	}

	public static byte[] StringToBytesArray(string str)
	{
		byte[] bytes = str.Split(';').Select(x => Convert.ToByte(x)).ToArray();
		return bytes;
	}

	public static bool isLocalAndCloudDataEqual(string str1, string str2)
	{
		return str1 == str2;
	}
}
