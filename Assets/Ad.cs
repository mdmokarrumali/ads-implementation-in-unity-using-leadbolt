using UnityEngine;
using System.Collections;

public class Ad : MonoBehaviour {

	void Start () {
        AppTrackerAndroid.startSession("YOUR_API_KEY"); // Please Add Your API Key
	}

	void OnGUI ()
	{

        if (GUILayout.Button("Create Interstitial Ad"))
        {
            CreateInterstitialAd();
        }

        if (GUILayout.Button("Create Alert"))
        {
            CreateAlert();
        }

        if (GUILayout.Button("Create Offer Wall"))
        {
            CreateOfferWall();
        }

        if (GUILayout.Button("Create Audio"))
        {
            CreateAudio();
        }

        if (GUILayout.Button("Floating Ad"))
        {
            CreateFloatingAd();
        }

        if (GUILayout.Button("Create Banner"))
        {
            CreateBanner();
        }

        if (GUILayout.Button("Create Reengagement Tools"))
        {
            CreateReengagementTools();
        }
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}

    /// <summary>
    /// Create Interstitial Ad
    /// </summary>
    public void CreateInterstitialAd()
    {
        AdController.initAdWithSectionId("YOUR_INTERSTITIAL_SECTION_ID", AdController.TYPE_DISPLAY);
        AdController.loadAd();  // To Display
    }

    public void CreateAlert()
    {
        AdController.initAdWithSectionId("YOUR_ALERT_SECTION_ID", AdController.TYPE_DISPLAY);
        AdController.loadAd();  // To Display
    }

    public void CreateOfferWall()
    {
        AdController.initAdWithSectionId("YOUR_OFFER_WALL_SECTION_ID", AdController.TYPE_DISPLAY);
        AdController.loadAd();  // To Display
    }

    public void CreateAudio()
    {
        AdController.initAdWithSectionId("YOUR_AUDIO_SECTION_ID", AdController.TYPE_AUDIO);
        AdController.loadAudioAd();  // To Play
    }

    public void CreateReengagementTools()
    {
        AdController.loadReEngagement("YOUR_REENGAGEMENT_SECTION_ID");
    }

    public void CreateFloatingAd()
    {
        AdController.initAdWithSectionId("YOUR-FLOATING_ID_SECTION_ID", AdController.TYPE_DISPLAY);
        AdController.loadAd();  // To Display
    }

    public void CreateBanner()
    {
        AdController.initAdWithSectionId("YOUR_BANNER_SECTION_ID", AdController.TYPE_DISPLAY);
        AdController.loadAd();  // To Display
    }
}
