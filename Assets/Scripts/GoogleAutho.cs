using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityEngine.UI;

public class GoogleAutho : MonoBehaviour
{
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
        Social.localUser.Authenticate((bool success) =>
        {
            countOfAuthentications++;
            Debug.Log(success);
            if (countOfAuthentications < 3)
            {
                if (success)
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
}

	
