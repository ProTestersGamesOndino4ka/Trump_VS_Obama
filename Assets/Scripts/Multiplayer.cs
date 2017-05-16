using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer : MonoBehaviour, RealTimeMultiplayerListener
{

    private bool showingWaitingRoom = false;
    public void OnRoomSetupProgress(float progress)
    {
      
        if (!showingWaitingRoom)
        {
            showingWaitingRoom = true;
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }
    }

    void RealTimeMultiplayerListener.OnRoomConnected(bool success)
    {

        GameObject.Find("Canvas/Text1").GetComponent<Text>().text = "Норма. все тута!";

        Message();
    }

    void RealTimeMultiplayerListener.OnLeftRoom()
    {
        throw new NotImplementedException();
    }

    void RealTimeMultiplayerListener.OnParticipantLeft(Participant participant)
    {
        throw new NotImplementedException();
    }

    void RealTimeMultiplayerListener.OnPeersConnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    void RealTimeMultiplayerListener.OnPeersDisconnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    void RealTimeMultiplayerListener.OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {   
        string decryptedData = System.Text.Encoding.UTF8.GetString(data);

        if (decryptedData == "onTap")
        {
            ScoreHandler.IncreaseEnemyScore();
        }
        else
        {
            GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = GameObject.Find("Canvas/Panel/" + decryptedData).GetComponent<Image>().sprite;
        }
    }
  


    public void InviteFriends()
    {
        const int MinOpponents = 1, MaxOpponents = 2;
        const int GameVariant = 0;
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
            GameVariant, this);
    }
    public void ViewInvite()
    {
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
    }
    public void Message()
    {
        byte[] message = System.Text.Encoding.UTF8.GetBytes(RaycastToImage._presidentImageHittedByRay.name);
        bool reliable = true;
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, message);
    }

   static public void MessageOnTap()
    {
        byte[] message = System.Text.Encoding.UTF8.GetBytes("onTap");
        bool reliable = true;
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, message);
    }
}