﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LocalRecords : MonoBehaviour
{

	public static  List<President> myPresidents = new List<President> ();
	public static  List<President> allPresidents;

	void Awake ()
	{
		allPresidents = new List<President> () {
			new President () {
				ID = 1,
				LastName = "Obama",
				Country = "USE",
				Price = "$0.99",
				ImageName = "obama",
				FlagName = "flag_usa"
			},
			new President () {
				ID = 2,
				LastName = "Putin",
				Country = "Russia",
				Price = "$0.99",
				ImageName = "putin",
				FlagName = "flag_rus"
			},
			new President () {
				ID = 3,
				LastName = "Trump",
				Country = "USA",
				Price = "$0.99",
				ImageName = "trump",
				FlagName = "flag_usa"
			},
			new President () {
				ID = 4,
				LastName = "Abe",
				Country = "Japan",
				Price = "$0.99",
				ImageName = "abe",
				FlagName = "flag_jp"
			},
			new President () {
				ID = 5,
				LastName = "Merkel",
				Country = "Germany",
				Price = "$0.99",
				ImageName = "merkel",
				FlagName = "flag_de"
			},
			new President () {
				ID = 6,
				LastName = "Lukashenko",
				Country = "Belarus",
				Price = "$0.99",
				ImageName = "batska",
				FlagName = "flag_bel"
			},
			new President () {
				ID = 7,
				LastName = "Erdogan",
				Country = "Turkey",
				Price = "$0.99",
				ImageName = "erdogan",
				FlagName = "flag_turk"
			},
			new President () {
				ID = 8,
				LastName = "Olland",
				Country = "France",
				Price = "$0.99",
				ImageName = "olland",
				FlagName = "flag_fr"
			},
			new President () {
				ID = 9,
				LastName = "Pena Nieto",
				Country = "Mexico",
				Price = "$0.99",
				ImageName = "pena_nieto",
				FlagName = "flag_mexica"
			},
			new President () {
				ID = 10,
				LastName = "Elizabeth II",
				Country = "Great Britain",
				Price = "$0.99",
				ImageName = "queen_elizabeth_2",
				FlagName = "flag_gb"
			},
		}; 
	}

	public static void SetMyPresidents ()
	{
		myPresidents = allPresidents.FindAll (x => SaveDataManager.GetLocalPresidentIDs ().Exists (y => y == x.ID));
		if (myPresidents == null || myPresidents.Count == 0) {
			SaveDataManager _data = new SaveDataManager ();
			_data.ReadDataFromFile ();
		}
	}

}
