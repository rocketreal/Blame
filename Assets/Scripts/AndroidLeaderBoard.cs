using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;


public class AndroidLeaderBoard : MonoBehaviour
{

    public Text connectStatusText;
    public void Start()
    {
        //listen for GooglePlayConnection events
        GooglePlayConnection.instance.addEventListener(GooglePlayConnection.PLAYER_CONNECTED, OnPlayerConnected);
        GooglePlayConnection.instance.addEventListener(GooglePlayConnection.PLAYER_DISCONNECTED, OnPlayerDisconnected);


        GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;


        //listen for GooglePlayManager events
        GooglePlayManager.instance.addEventListener(GooglePlayManager.ACHIEVEMENT_UPDATED, OnAchievementUpdated);
        GooglePlayManager.instance.addEventListener(GooglePlayManager.SCORE_SUBMITED, OnScoreSubmited);
        GooglePlayManager.instance.addEventListener(GooglePlayManager.SCORE_REQUEST_RECEIVED, OnScoreUpdated);




        GooglePlayManager.ActionSendGiftResultReceived += OnGiftResult;
        GooglePlayManager.ActionPendingGameRequestsDetected += OnPendingGiftsDetected;
        GooglePlayManager.ActionGameRequestsAccepted += OnGameRequestAccepted;


        GooglePlayManager.ActionOAuthTokenLoaded += ActionOAuthTokenLoaded;
        GooglePlayManager.ActionAvailableDeviceAccountsLoaded += ActionAvailableDeviceAccountsLoaded;

        GooglePlayManager.instance.addEventListener(GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchievmnetsLoadedInfoListner);


        if (GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED)
        {
            //checking if player already connected
            OnPlayerConnected();
        }
    }

    private void OnAchievmnetsLoadedInfoListner()
    {
        throw new NotImplementedException();
    }

    private void ActionAvailableDeviceAccountsLoaded(List<string> obj)
    {
        throw new NotImplementedException();
    }

    private void ActionOAuthTokenLoaded(string obj)
    {
        throw new NotImplementedException();
    }

    private void OnGameRequestAccepted(List<GPGameRequest> obj)
    {
        throw new NotImplementedException();
    }

    private void OnPendingGiftsDetected(List<GPGameRequest> obj)
    {
        throw new NotImplementedException();
    }

    private void OnGiftResult(GooglePlayGiftRequestResult obj)
    {
        throw new NotImplementedException();
    }

    private void OnScoreUpdated()
    {
        throw new NotImplementedException();
    }

    private void OnScoreSubmited()
    {
        throw new NotImplementedException();
    }

    private void OnAchievementUpdated()
    {
        throw new NotImplementedException();
    }

    private void ActionConnectionResultReceived(GooglePlayConnectionResult obj)
    {
        throw new NotImplementedException();
    }

    private void OnPlayerDisconnected()
    {
        throw new NotImplementedException();
    }

    private void OnPlayerConnected()
    {
        connectStatusText.text = "Connect sussesfull!";
    }
}
