using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManagersScripts : MonoBehaviour
{
    public GameObject _player;
    string gameId = "3668367";
    bool testMode = false;

    public void showRewardedAds()
    {
        Advertisement.Initialize(gameId, testMode);
        string placementID = "rewardedVideo";
        if (Advertisement.IsReady(placementID))
        {
            var options = new ShowOptions
            {
                resultCallback = MakeAdsResult
            };
            Advertisement.Show(placementID, options);
        }

    }
    public void Givekey_01()
    {
        Advertisement.Initialize(gameId, testMode);
        string placementID = "rewardedVideo";
        if (Advertisement.IsReady(placementID))
        {
            var options = new ShowOptions
            {
                resultCallback = GiveOneKeyToPlayer
            };
            Advertisement.Show(placementID, options);
        }
    }
    private void MakeAdsResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("Ads is Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ads is Skip");
                break;
            case ShowResult.Finished:
                Debug.Log("Ads is Finised");
                Add_100G();
                break;
        }
    }
    private void GiveOneKeyToPlayer(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("Ads is Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ads is Skip");
                break;
            case ShowResult.Finished:
                Debug.Log("Ads is Finised");
                GiveOneKey();
                break;
        }
    }
    void Add_100G()
    {
        PlayerScripts player = _player.GetComponent<PlayerScripts>();
        if (player != null)
        {
            player.diamonds += 100;
        }
    }

    void GiveOneKey()
    {
        GateScripts._gateScripts.PlayerKeyText.text = "1";
    }

}
