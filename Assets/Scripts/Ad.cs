#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections; 
using AppTrackerUnitySDK;

public class Ad : MonoBehaviour
{
	private bool hasInterstitial = false;
	private bool hasRewardedVideo = false;
//	private int count = -1;

	void Start ()
	{
		Screen.orientation = ScreenOrientation.AutoRotation; //to allow orientation other than portrait
		#if UNITY_ANDROID
		AppTrackerAndroid.startSession ("jfjZbrPtdR3mcZW9r83Bx9pB8I5PXvpb"); // Please change this demo API Key to your own API Key
		AppTrackerAndroid.onModuleClosedEvent += onModuleClosedEvent;
		AppTrackerAndroid.onModuleFailedEvent += onModuleFailedEvent;
		AppTrackerAndroid.onModuleLoadedEvent += onModuleLoadedEvent;
		AppTrackerAndroid.onModuleCachedEvent += onModuleCachedEvent;
		#endif
	}
	
	void OnGUI ()
	{ 
		float w = Screen.width;
		float h = Screen.height;
		
		float scale = Mathf.Min (4.0f, Mathf.Min (w / 240.0f, h / 210.0f));
		GUI.matrix = Matrix4x4.Scale (new Vector3 (scale, scale, 1));
		
		float width = (int)(w / scale) - 30;
		
		
		// setup buttons
		Texture2D logo = Resources.Load ("head") as Texture2D;
		GUILayout.Label (logo, GUILayout.Height (150), GUILayout.Width (316));
		
		GUILayout.Space (5);
		GUILayout.Label ("Interstitial Ad Ready: " + hasInterstitial);
		if (GUILayout.Button ("Cache Interstitial", GUILayout.Width (width))) {
			AppTrackerAndroid.loadModuleToCache ("inapp");
		}
		if (GUILayout.Button ("Load Interstitial", GUILayout.Width (width))) {
			if (AppTrackerAndroid.isAdReady ("inapp")) {
				AppTrackerAndroid.loadModule ("inapp");
			}
		}
		
		GUILayout.Space (5);
		GUILayout.Label ("Rewarded Video Ready: " + hasRewardedVideo);
		if (GUILayout.Button ("Cache Rewarded Video", GUILayout.Width (width))) {
			AppTrackerAndroid.loadModuleToCache ("video");
		}
		if (GUILayout.Button ("Show Rewarded Video", GUILayout.Width (width))) {
			if (AppTrackerAndroid.isAdReady ("video")) {
				AppTrackerAndroid.loadModule ("video");
			}
		}
	}
	
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();

//		// check once in a while if ad is available
//		if (count == -1 || count > 80) {
//			hasInterstitial = AppTrackerAndroid.isAdReady ("inapp");
//			hasRewardedVideo = AppTrackerAndroid.isAdReady ("video");
//			
//			count = 0;
//		}
//		count++;
	}
	
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
			} else if (placement.Equals ("video")) {
				hasRewardedVideo = true;
			}
		}
	}

	void onModuleLoadedEvent (string placement)
	{
		print ("onModuleLoaded:" + placement);  
		if (placement.Equals ("inapp")) {
			hasInterstitial = false;
		} else if (placement.Equals ("video")) {
			hasRewardedVideo = false;
		}
	}

	void onModuleCachedEvent (string placement)
	{
		if (placement.Equals ("inapp")) {
			hasInterstitial = true;
		} else if (placement.Equals ("video")) {
			hasRewardedVideo = true;
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
}
