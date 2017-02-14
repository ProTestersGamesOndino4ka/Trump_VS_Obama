using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FreePresidentScript : MonoBehaviour
{

    FreePresident freePresident;
    GameObject textClickPoints;
    GameObject timeAds;
 
    void Start()
    { 
        timeAds = GameObject.Find("Canvas/TimeAds");
        textClickPoints = GameObject.Find("Canvas/TextClickPoints");

        if (PlayerPrefs.GetString("TimeFreePresident") == string.Empty || PlayerPrefs.GetString("TimeFreePresident") == null)
        {
            PlayerPrefs.SetString("TimeFreePresident", DateTime.Now.AddMinutes(-10).ToString());
        }
        if (SaveDataManager.GetPoints() < 99)
        {           
            GameObject.Find("Canvas/TextViewAds").GetComponent<Text>().text = "Click on gift to get 50points";
        }
        else
        {
            GameObject.Find("Canvas/TextOpenGift").GetComponent<Text>().text = "Open me";
        } 
        Update();
    }

    void Update()
    {
        textClickPoints.GetComponent<Text>().text = SaveDataManager.GetPoints().ToString();
        if (Convert.ToDateTime(PlayerPrefs.GetString("TimeFreePresident")).AddMinutes(10) > DateTime.Now && SaveDataManager.GetPoints() < 99)
        {
            timeAds.GetComponent<Text>().text = Convert.ToString(Convert.ToDateTime(PlayerPrefs.GetString("TimeFreePresident")).AddMinutes(10) - DateTime.Now).Remove(8);                        
        }
        else if (SaveDataManager.GetPoints() < 99)
        {
            timeAds.GetComponent<Text>().text = "ads ready";           
        } 
    }
}
