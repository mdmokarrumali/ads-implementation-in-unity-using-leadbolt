using UnityEngine;
using System.Collections;
using AppTrackerUnitySDK;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
	public Text txtIsInterstitialAvail;
	public Text txtRewardedVideoAvail;
	public Text txtOfferWallAvail;
	private bool hasInterstitial = false;
	private bool hasRewardedVideo = false;
	private bool hasOfferWall = false;
	private int count = -1;
	
	// Use this for initialization
	void Start ()
	{
		AppTrackerAndroid.startSession ("ENTER_YOUR_API_KEY"); // Please change this demo API Key to your own API Key
	}
	
	void OnEnable ()
	{
		hasInterstitial = false;
		hasRewardedVideo = false;
		hasOfferWall = false;
	
		#if UNITY_ANDROID
		AppTrackerAndroid.onModuleClosedEvent += onModuleClosedEvent;
		AppTrackerAndroid.onModuleFailedEvent += onModuleFailedEvent;
		AppTrackerAndroid.onModuleLoadedEvent += onModuleLoadedEvent;
		AppTrackerAndroid.onModuleCachedEvent += onModuleCachedEvent;
		#endif
	}
	
	void OnDisable ()
	{
		#if UNITY_ANDROID
		AppTrackerAndroid.onModuleClosedEvent -= onModuleClosedEvent;
		AppTrackerAndroid.onModuleFailedEvent -= onModuleFailedEvent;
		AppTrackerAndroid.onModuleLoadedEvent -= onModuleLoadedEvent;
		AppTrackerAndroid.onModuleCachedEvent -= onModuleCachedEvent;
		#endif
	}
	
	#region Leadbolt Callback Methods
	void onModuleClosedEvent (string placement)
	{
		print ("onModuleClosed:" + placement);  
	}
	
	void onModuleFailedEvent (string placement, string error, bool cached)
	{
		print ("onModuleFailed:" + placement + ":" + error + ":" + cached);  
		if (cached) {
			if (placement.Equals ("inapp")) {
				hasInterstitial = true;
				txtIsInterstitialAvail.text = "is available  : " + hasInterstitial;
			} else if (placement.Equals ("video")) {
				hasRewardedVideo = true;
				txtRewardedVideoAvail.text = "is available  : " + hasRewardedVideo;
			}
		}
	}
	
	void onModuleLoadedEvent (string placement)
	{
		print ("onModuleLoaded:" + placement);  
		if (placement.Equals ("inapp")) {
			hasInterstitial = false;
			txtIsInterstitialAvail.text = "is available  : " + hasInterstitial;
		} else if (placement.Equals ("video")) {
			hasRewardedVideo = false;
			txtRewardedVideoAvail.text = "is available  : " + hasRewardedVideo;
		}
	}
	
	void onModuleCachedEvent (string placement)
	{
		if (placement.Equals ("inapp")) {
			hasInterstitial = true;
			txtIsInterstitialAvail.text = "is available  : " + hasInterstitial;
		} else if (placement.Equals ("video")) {
			hasRewardedVideo = true;
			txtRewardedVideoAvail.text = "is available  : " + hasRewardedVideo;
		} 
		print ("onModuleCached:" + placement);  
	}
	
	void onModuleClickedEvent (string placement)
	{
		print ("onModuleClicked:" + placement);
	}
	
	void onMediaFinishedEvent (bool viewCompleted)
	{
		print ("onMediaFinished:" + viewCompleted);
	}
	#endregion
	
	#region Public_Methods
	public void btnCacheInterstitialTapped ()
	{
		AppTrackerAndroid.loadModuleToCache ("inapp");
	}
	
	public void btnShowInterstitialTapped ()
	{
		if (AppTrackerAndroid.isAdReady ("inapp")) {
			AppTrackerAndroid.loadModule ("inapp");
		}
	}
	
	public void btnCacheRewardedVideoTapped ()
	{
		AppTrackerAndroid.loadModuleToCache ("video");
	}
	
	public void btnShowRewardedVideoTapped ()
	{
		if (AppTrackerAndroid.isAdReady ("video")) {
			AppTrackerAndroid.loadModule ("video");
		}
	}
	#endregion
}