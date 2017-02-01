using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LocalRecords : MonoBehaviour
{

	static string _txt = string.Empty;
	static string FILE_NAME = "records.txt";
	static int[] records;
	public static  List<President> myPresidents = new List<President> ();
	public static  List<President> allPresidents;

	void Start ()
	{
		allPresidents = new List<President> () {
			new President () { ID = 1, LastName = "Obama", Country = "USE", Price = "0.99$", ImageName = "obama" },
			new President () { ID = 2, LastName = "Putin", Country = "Russia", Price = "0.99$", ImageName = "putin" },
			new President () { ID = 3, LastName = "Trump", Country = "USA", Price = "0.99$", ImageName = "trump" },
			new President (){ ID = 4, LastName = "Abe", Country = "Japan", Price = "0.99$", ImageName = "abe" },
			new President (){ ID = 5, LastName = "Merkel", Country = "Germany", Price = "0.99$", ImageName = "merkel" },
			new President (){ ID = 6, LastName = "Lukashenko", Country = "Belarus", Price = "0.99$", ImageName = "batska" },
		};

		ReadFile ();
		AddMyPresident (allPresidents);   
	}

	void ReadFile ()
	{
#if UNITY_EDITOR
		string path = Application.dataPath + "/" + FILE_NAME;
#else
		string path = Application.persistentDataPath + "/" + FILE_NAME;
#endif
		if (!File.Exists (path)) {
			var fileWriter = File.CreateText (path);
			fileWriter.Write ("1;2;3");
          
			fileWriter.Close ();
		}
		var fileReader = File.OpenText (path);
		records = fileReader.ReadToEnd ().Split (new string[] { ";" }, StringSplitOptions.None).Select (s => int.Parse (s)).ToArray ();
		fileReader.Close ();
	}

	void AddMyPresident (List <President> president)
	{
		foreach (var member in president) {
			for (int i = 0; i < records.GetLength (0); i++) {
				if (member.ID == records [i]) {
					myPresidents.Add (member);
				}
			}
		}
	}

	void Update ()
	{
		
	}
}
