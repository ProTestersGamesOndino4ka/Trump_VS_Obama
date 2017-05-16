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
	public static bool freePresident = false;
	public float scrollSpeed = -20;
	private RaycastHit2D _hit;
	public Image prizeImage;
	int countCheck = 0;


	void Update()
	{ 
		if(freePresident)
		{
			scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, 2f * Time.deltaTime);
			scrollPanel.transform.Translate(new Vector2(scrollSpeed, 0) * Time.deltaTime);
		}           
		if(scrollSpeed == 0 && countCheck == 0)
		{
			countCheck++;
			_hit = Physics2D.Raycast(Vector2.down, Vector2.up);
			if(_hit.collider != null)
			{               
				prizeImage.sprite = _hit.collider.gameObject.GetComponent<Image>().sprite;
				panelPrize.SetActive(true);
				GameObject.Find("Canvas/PanelPrize/PrizeButton/Text").GetComponent<Text>().text = LocalRecords.allPresidents.Find(X => X.ImageName == prizeImage.sprite.name).LastName;
				Payments.Buy(LocalRecords.allPresidents.Find(X => X.ImageName == prizeImage.sprite.name).ID);
                
			}
			else
			{
				scrollSpeed = scrollSpeed - 0.1f;
			}
		}
	}


	public void FreePresidentButton()
	{
		if(SaveDataManager.GetPoints() > 99)
		{
			GameObject.Find("Canvas/Button_Gift").GetComponent<Animation>().Stop();
			GameObject.Find("Canvas/TextOpenGift").SetActive(false);
			GameObject.Find("Canvas/Button_Gift/Particle System").SetActive(false);
			GameObject.Find("Canvas/TimeAds").SetActive(false);
			SaveDataManager.SetPoints(-100);
			GenerationRandomPresident();         
			SaveDataManager.SaveUserData();          
		}
		else
		{
			ShowAds();
			//Не забыть убрать от сюда!!!!!
			SaveDataManager.SetPoints(50);
			SaveDataManager.SaveUserData();
			PlayerPrefs.SetString("TimeFreePresident", DateTime.Now.ToString());
			CloseTab();
		}
	}

	void ShowAds()
	{
		IPStatus status = IPStatus.Unknown;
		try
		{
			status = new System.Net.NetworkInformation.Ping().Send("www.google.com").Status;
		}
		catch
		{
		}
		if(status == IPStatus.Success)
		{
			// Ads.Show();         
			SaveDataManager.SetPoints(50);
			SaveDataManager.SaveUserData();
			CloseTab();
		}
		else
		{
			//GameObject.Find("Canvas/Text_StatusIP").GetComponent<Text>().text = "нет соединения с интернетом";
		}
	}

	public void CloseTab()
	{
		LoadScene.LoadFreePresidentScene();
	}


	public void GenerationRandomPresident()
	{
		freePresident = true;      
		gameObject.SetActive(true);
       
		for (int i = 0; i < 40; i++)
		{
			int random = UnityEngine.Random.Range(1, 100);
			int randomPresident = 0;
			if(random <= 10)
			{
				randomPresident = 0;
			}
			else if(10 < random && random < 20)
			{
				randomPresident = 1;
			}
			else if(20 < random && random < 30)
			{
				randomPresident = 2;
			}
			else if(30 < random && random < 40)
			{
				randomPresident = 3;
			}
			else if(40 < random && random < 50)
			{
				randomPresident = 4;
			}
			else if(50 < random && random < 60)
			{
				randomPresident = 5;
			}
			else if(60 < random && random < 70)
			{
				randomPresident = 6;
			}
			else if(85 < random && random <= 90)
			{
				randomPresident = 7;
			}
			else if(90 < random && random <= 95)
			{
				randomPresident = 8;
			}
			else if(95 < random && random <= 99)
			{
				randomPresident = 9;
			}
			obj = Instantiate(presidentImage[randomPresident], new Vector2(0, 0), Quaternion.identity) as GameObject;
			obj.transform.SetParent(scrollPanel.transform);
			obj.transform.localScale = new Vector2(1, 1);           
		}
	}
}