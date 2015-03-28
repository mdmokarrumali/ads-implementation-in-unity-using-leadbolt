using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class AppTrackerAndroid : MonoBehaviour {

	private static AndroidJavaObject appTracker;

	void Awake()
	{
		gameObject.name = "AppTrackerAndroid";
		DontDestroyOnLoad(this);
	}

	private static void initializeAppTracker()
	{
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		appTracker = new AndroidJavaObject("com.appfireworks.unity.android.AppFireworksUnity", jo);
	}

	public static void startSession(string apikey)
	{
		initializeAppTracker();
		appTracker.Call("startSession",apikey);
	}

	public static void closeSession(bool sync)
	{
		if(appTracker != null)
			appTracker.Call("closeSession", sync);
	}
	public static void pause()
	{
		if(appTracker != null)
			appTracker.Call("pause");
	}
	public static void resume()
	{
		if(appTracker != null)
			appTracker.Call("resume");
	}
	public static void Event(string name)
	{
		if(appTracker != null)
			appTracker.Call("event", name);
	}
	public static void Event(string name, float value)
	{
		if(appTracker != null)
			appTracker.Call("event", name, value);
	}
	public static void transaction(string name, float value, string currencycode)
	{
		if(appTracker != null)
			appTracker.Call("transaction", name, value, currencycode);
	}
	public static void loadModule(string locationcode)
	{
		if(appTracker != null)
			appTracker.Call("loadModule", locationcode);
	}
	public static void loadModuleToCache(string locationcode)
	{
		if(appTracker != null)
			appTracker.Call("loadModuleToCache", locationcode);
	}
	public static void destroyModule()
	{
		if(appTracker != null)
			appTracker.Call("destroyModule");
	}

	public static event Action onModuleLoadedEvent;
	public static event Action onModuleFailedEvent;
	public static event Action onModuleClosedEvent;
	public static event Action onModuleCachedEvent;
	
	public void onModuleLoaded(string message)
	{
		if(onModuleLoadedEvent != null)
			onModuleLoadedEvent();
	}
	public void onModuleFailed(string message)
	{
		if(onModuleFailedEvent != null)
			onModuleFailedEvent();	
	}
	public void onModuleClosed(string message)
	{
		if(onModuleClosedEvent != null)
			onModuleClosedEvent();
	}
	public void onModuleCached(string message)
	{
		if(onModuleCachedEvent != null)
			onModuleCachedEvent();
	}
}