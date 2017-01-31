using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LocalRecords : MonoBehaviour {

   static string _txt = string.Empty;
   static string FILE_NAME = "records.txt";
   static int[] records;
    static public List<President> myPresident = new List<President>();

    void Start () {

        List<President> presidents = new List<President>() {
            new President() { ID = 1, LastName = "Obama", Country = "USE", Price = "0.99$", ImageName = "obama" },
            new President() { ID = 2, LastName = "Putin", Country = "Russia", Price = "0.99$", ImageName = "putin" },
            new President() { ID = 3, LastName = "Trump", Country = "USA", Price = "0.99$", ImageName = "trump" }
              
    };

        //presidents.Add(new President() { ID = 1, LastName = "", Country = "", Price = "1$", ImageName = "obama" });
        ReadFile();
        AddMyPresident(presidents);
   
    }

    void ReadFile()
    {
#if UNITY_EDITOR
        string path = Application.dataPath + "/" + FILE_NAME;
#else
		string path = Application.persistentDataPath + "/" + FILE_NAME;
#endif
        if (!File.Exists(path))
        {
            var fileWriter = File.CreateText(path);
            fileWriter.Write("1;2;3");
          
            fileWriter.Close();
        }
        var fileReader = File.OpenText(path);
            records = fileReader.ReadToEnd().Split(new string[] { ";" }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();
        fileReader.Close();
    }

    void AddMyPresident(List <President> president)
    {
        foreach (var member in president)
        {
            for (int i = 0; i < records.GetLength(0); i++)
            {
                if (member.ID == records[i])
                {
                    myPresident.Add(member);
                }
            }
        }
    }
    void Update () {
		
	}
}
