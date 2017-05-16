using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityEngine.UI;

public class GoogleAutho : MonoBehaviour, RealTimeMultiplayerListener
{
	public void OnLeftRoom()
	{
		throw new NotImplementedException();
	}

	public void OnParticipantLeft(Participant participant)
	{
		throw new NotImplementedException();
	}

	public void OnPeersConnected(string[] participantIds)
	{
		throw new NotImplementedException();
	}

	public void OnPeersDisconnected(string[] participantIds)
	{
		throw new NotImplementedException();
	}

	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
	{
		string str = System.Text.Encoding.UTF8.GetString(data);
		//Вместо этого нужно будет просто вызвать метод, увеличивающий количество очков противника 
		//EnemyScoreText.scoreValue = Convert.ToInt32 (str);
		//GameObject.Find("Canvas/Button_Check/Text").GetComponent<Text>().text = str.ToString();
	}

	public void OnRoomConnected(bool success)
	{
		if(success)
		{
			GameObject.Find("Canvas/Button_Achivment/Text").GetComponent<Text>().text = "Соединение установлено";
		}
		else
		{
			GameObject.Find("Canvas/Button_Achivment/Text").GetComponent<Text>().text = "Соединение НЕ установлено";
		}
     
	}

	public void OnRoomSetupProgress(float percent)
	{
		throw new NotImplementedException();
	}

	void Awake()
	{
		int countOfAuthentications = 0;
		try
		{
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
				.EnableSavedGames()
				.Build();
			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.Activate();
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning("Unhandled exception on config Initializing" + ex.Message);
		}
		Social.localUser.Authenticate((bool success) => {
			countOfAuthentications++;
			Debug.Log(success);
			if(countOfAuthentications < 3)
			{
				if(success)
				{
					GoogleCloudSystem _cloud = new GoogleCloudSystem();
					_cloud.Initialize();
				}
				else
				{
					SaveDataManager.ReadDataFromFile(true);
				}
			}
		});

	}

	public void Connect()
	{
		const int MinOpponents = 1 , MaxOpponents= 3;
		const int GameVariant = 0;
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
			GameVariant, this);
	}

	public void ViewInvite()
	{
		PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
	}

	static public void Message()
	{
       
		//См. 36 строку
		/*
		 * byte[] message = System.Text.Encoding.UTF8.GetBytes(LocalScoreText.scoreValue.ToString());           
		   bool reliable = false;
		   PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, message);*/
	}

	public void Achievment()
	{
		Social.ShowAchievementsUI();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
