using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Net.NetworkInformation;
using System.IO;


public class FreePresident : MonoBehaviour
{
	int clickPoints = 1000;
	Image imageFreePresident;

	void Start ()
	{
		imageFreePresident = GetComponent<Image> ();
		GameObject.Find ("Canvas/Text_ClickPoint").GetComponent<Text> ().text = clickPoints.ToString ();
		CheckCountClickPoints ();
	}

	/*
	public void GetRandomPresident ()
	{
		if (clickPoints > 999) {
			System.Random rnd = new System.Random ();
			byte random = (byte)rnd.Next (1, 7);
			foreach (var resource in LocalPresidentImage._list) { // Того листа там уже нет. Пока закомментил
				if (resource.name == LocalRecords.allPresidents.Find (x => x.ID == random).ImageName) {
					imageFreePresident.sprite = resource;
				}
			}

			if (LocalRecords.records.Any (x => x == random) != true) {
#if UNITY_EDITOR
				string path = Application.dataPath + "/" + "records.txt";
#else
		string path = Application.persistentDataPath + "/" + "records.txt";
#endif
				using (StreamWriter writer = File.AppendText (path))
					writer.Write (";{0}", random);
			}
			LocalRecords.ReadFile ();
			clickPoints = clickPoints - 1000;
			GameObject.Find ("Canvas/Text_ClickPoint").GetComponent<Text> ().text = clickPoints.ToString ();
			CheckCountClickPoints ();
		} else {
			IPStatus status = IPStatus.Unknown;
			try {
				status = new System.Net.NetworkInformation.Ping ().Send ("www.google.com").Status;
			} catch {
			}

			if (status == IPStatus.Success) {
				//ShowReklama();
				GameObject.Find ("Canvas/Text_StatusIP").GetComponent<Text> ().text = "success";
				clickPoints += 200;//ибо 1000 как-то дохера
				CheckCountClickPoints ();
				GameObject.Find ("Canvas/Text_ClickPoint").GetComponent<Text> ().text = clickPoints.ToString ();
			} else {
				GameObject.Find ("Canvas/Text_StatusIP").GetComponent<Text> ().text = "нет соединения с интернетом";
			}
		}
	}*/

	void CheckCountClickPoints ()
	{
		if (clickPoints > 999) {
			GameObject.Find ("Canvas/Button_RandomPresident/Text").GetComponent<Text> ().text = "free president -100 points";
		} else {
			GameObject.Find ("Canvas/Button_RandomPresident/Text").GetComponent<Text> ().text = "VIEW ads get 100 points";
		}
	}
}
