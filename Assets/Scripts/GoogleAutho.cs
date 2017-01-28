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
        side_1Text.scoreValue = Convert.ToInt32(str);
        GameObject.Find("Canvas/Button_Check/Text").GetComponent<Text>().text = str.ToString();
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            GameObject.Find("Canvas/Button_Achivment/Text").GetComponent<Text>().text = "Соединение установлено" ;
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

    // Use this for initialization
    void Start () {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            Debug.Log(success);
        });
    }

    public void Connect()
    {
        const int MinOpponents = 1, MaxOpponents = 3;
        const int GameVariant = 0;
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
                    GameVariant, this);
    }
    public void ViewInvite()
    {

        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
    }

     static public void Message() {
       
        byte[] message = System.Text.Encoding.UTF8.GetBytes(side_0Text.scoreValue.ToString());
           
        bool reliable = false;
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, message);
    }

    public void Achievment()
    {
        Social.ShowAchievementsUI();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
