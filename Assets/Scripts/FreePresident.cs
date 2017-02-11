using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Net.NetworkInformation;
using System.IO;
using UnityEngine.SceneManagement;

public class FreePresident : MonoBehaviour
{

    public GameObject[] presidentImage;
    public GameObject scrollPanel;
    public GameObject panelPrize;
    private GameObject obj;
    private bool freePresident = false;
    public float scrollSpeed = -20;
    private RaycastHit2D _hit;
    public Image prize;
    
    

   
    public void FreePresidentButton()
    {
        if (DataParser.GetPoints() > 99)
        {
            gameObject.SetActive(true);
            freePresident = true;
            GenerateFreePresident();
            DataParser.SetPoints(-100);
            new GooglePlayGames_CloudSystem().SaveDataToCloud(new DataParser());
        }
        else
        {
            //ShowReklama
            DataParser.SetPoints(50);
            new GooglePlayGames_CloudSystem().SaveDataToCloud(new DataParser());
            PlayerPrefs.SetString("TimeFreePresident", DateTime.Now.ToString());
            CloseTab(); 
        }   
    }

    public void CloseTab ()
     {      
        SceneManager.LoadScene("free_president");
    }
    void Start()
    {
        CheckCountClickPoints();
        Update();
    }
    void Update()
    {
        if (Convert.ToDateTime(PlayerPrefs.GetString("TimeFreePresident")).AddMinutes(10) > DateTime.Now)
        {
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = Convert.ToString(Convert.ToDateTime(PlayerPrefs.GetString("TimeFreePresident")).AddMinutes(10) - DateTime.Now);
        }
        if (freePresident)
        {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, 2f * Time.deltaTime);
            scrollPanel.transform.Translate(new Vector2(scrollSpeed, 0) * Time.deltaTime);
        }
        if (scrollSpeed == 0)
        {
            _hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if (_hit.collider != null)
            {              
                prize.sprite = _hit.collider.gameObject.GetComponent<Image>().sprite;
                panelPrize.SetActive(true);
                //Payments.Buy(LocalRecords.allPresidents.Find(X=>X.ImageName == prize.sprite.name).ID);
            }
            else 
            {
                scrollSpeed = scrollSpeed - 0.1f;
            }
        }
    }
    public void GenerateFreePresident()
    {
        for (int i = 0; i < 60; i++)
        {
            int random = UnityEngine.Random.Range(1, 100);
            int randomPresident=0;
            if (random <= 10)
            {
                randomPresident = 0;
            }
            else if (10 < random && random < 20)
            {
                randomPresident = 1;
            }
            else if (20 < random && random < 30)
            {
                randomPresident = 2;
            }
            else if (30 < random && random < 40)
            {
                randomPresident = 3;
            }
            else if (40 < random && random < 50)
            {
                randomPresident = 4;
            }
            else if (50 < random && random < 60)
            {
                randomPresident = 5;
            }
            else if (60 < random && random < 70)
            {
                randomPresident = 6;
            }
            else if (85 < random && random <= 90)
            {
                randomPresident = 7;
            }
            else if (90 < random && random <= 95)
            {
                randomPresident = 8;
            }
            else if (95 < random && random <= 99)
            {
                randomPresident = 9;
            }
            obj = Instantiate(presidentImage[randomPresident], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(scrollPanel.transform);
            obj.transform.localScale = new Vector2(1,1);           
        }
    }

    void CheckCountClickPoints()
    {
        if (DataParser.GetPoints() > 99)
        {
            GameObject.Find("Canvas/Button_RandomPresident/Text").GetComponent<Text>().text = "free president -100 points";
        }
        else
        {
            GameObject.Find("Canvas/Button_RandomPresident/Text").GetComponent<Text>().text = "VIEW ads get 50 points";
        }
    }


    void Buy()
    {
        IPStatus status = IPStatus.Unknown;
        try
        {
            status = new System.Net.NetworkInformation.Ping().Send("www.google.com").Status;
        }
        catch
        {
        }

        if (status == IPStatus.Success)
        {
          //  ShowReklama();
            GameObject.Find("Canvas/Text_StatusIP").GetComponent<Text>().text = "success";
          //  clickPoints += 200;//ибо 1000 как-то дохера
           //new GooglePlayGames_CloudSystem().SaveDataToCloud(new DataParser ());
            CheckCountClickPoints();
          //  GameObject.Find("Canvas/Text_ClickPoint").GetComponent<Text>().text = clickPoints.ToString();
        }
        else
        {
            GameObject.Find("Canvas/Text_StatusIP").GetComponent<Text>().text = "нет соединения с интернетом";
        }
    }
}

//void Start()
//{
//    imageFreePresident = GetComponent<Image>();
//    GameObject.Find("Canvas/Text_ClickPoint").GetComponent<Text>().text = clickPoints.ToString();
//    CheckCountClickPoints();
//}


//    public void GetRandomPresident()
//    {
//        if (clickPoints > 999)
//        {
//            System.Random rnd = new System.Random();
//            byte random = (byte)rnd.Next(1, 7);
//            foreach (var resource in LocalPresidentImage._list)
//            { // Того листа там уже нет. Пока закомментил
//                if (resource.name == LocalRecords.allPresidents.Find(x => x.ID == random).ImageName)
//                {
//                    imageFreePresident.sprite = resource;
//                }
//            }

//            if (LocalRecords.records.Any(x => x == random) != true)
//            {
//#if UNITY_EDITOR
//                string path = Application.dataPath + "/" + "records.txt";
//#else
//		string path = Application.persistentDataPath + "/" + "records.txt";
//#endif
//                using (StreamWriter writer = File.AppendText(path))
//                    writer.Write(";{0}", random);
//            }
//            LocalRecords.ReadFile();
//            clickPoints = clickPoints - 1000;
//            GameObject.Find("Canvas/Text_ClickPoint").GetComponent<Text>().text = clickPoints.ToString();
//            CheckCountClickPoints();
//        }
//        else
//        {
//            IPStatus status = IPStatus.Unknown;
//            try
//            {
//                status = new System.Net.NetworkInformation.Ping().Send("www.google.com").Status;
//            }
//            catch
//            {
//            }

//            if (status == IPStatus.Success)
//            {
//                ShowReklama();
//                GameObject.Find("Canvas/Text_StatusIP").GetComponent<Text>().text = "success";
//                clickPoints += 200;//ибо 1000 как-то дохера
//                CheckCountClickPoints();
//                GameObject.Find("Canvas/Text_ClickPoint").GetComponent<Text>().text = clickPoints.ToString();
//            }
//            else
//            {
//                GameObject.Find("Canvas/Text_StatusIP").GetComponent<Text>().text = "нет соединения с интернетом";
//            }
//        }
//    }

