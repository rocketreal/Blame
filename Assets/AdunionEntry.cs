using UnityEngine;
using System.Collections;

public class AdunionEntry : MonoBehaviour
{	

	public string point = "point";
	public string resultStr = "";

	void Awake(){

		/*
			set the name of GameObject so that the object can recevie result of ad
		 */
		Adunion4Unity.Instance.setHandleName("Main Camera");
	}

	void OnGUI ()
	{
		#if UNITY_ANDROID
		GUI.skin.label.fontSize = Screen.height / 50;
		GUI.skin.button.fontSize = Screen.height / 50;

		GUI.skin.textField.fontSize = Screen.height/25;
		GUI.skin.textField.alignment = TextAnchor.MiddleCenter;

		GUILayout.Label("Interstitial Ad" , GUILayout.Height(Screen.height / 12));
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("New Interstitial Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width*3/4))){
			Adunion4Unity.Instance.showInterstitial(point);
		}

		point = GUI.TextField(new Rect(Screen.width*3/4 + 3, Screen.height / 12 + 5, Screen.width/4 - 3, Screen.height / 12-3), point);

		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Show 'gamestart' Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Show interstitial ad 'gamestart'
			Adunion4Unity.Instance.showInterstitialAd(Adunion4Unity.IAD_TYPE_GAMESTART);

		}		
		if (GUILayout.Button("Show 'gamepause' Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{ 
			// Show interstitial ad 'gamepause'
			Adunion4Unity.Instance.showInterstitialAd(Adunion4Unity.IAD_TYPE_GAMEPAUSE);
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Show game gift", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Show interstitial ad 'gamegift'
			Adunion4Unity.Instance.showInterstitialAd(Adunion4Unity.IAD_TYPE_GAMEGIFT);
		}
		if (GUILayout.Button("Show 'gameexit' Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// stop loading interstitial ad
			Adunion4Unity.Instance.showInterstitialAd(Adunion4Unity.IAD_TYPE_GAMEEXIT);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Preload Interstitial", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// stop loading interstitial ad
			Adunion4Unity.Instance.preloadInterstitialAd();
		}
		if (GUILayout.Button("StopLoading", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// stop loading interstitial ad
			Adunion4Unity.Instance.stopLoadingInterstitialAd();
		}
		GUILayout.EndHorizontal();


		GUILayout.Label("Banner Ad", GUILayout.Height(Screen.height / 12));
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Preload Banner", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Preload banner ad
			Adunion4Unity.Instance.preloadBannerAd();
		}

		if (GUILayout.Button("Show Banner Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Show banner ad
			Adunion4Unity.Instance.showBannerAd(Adunion4Unity.BAD_POS_BOTTOM_CENTER);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Close Banner Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Close banner ad
			Adunion4Unity.Instance.closeBannerAd();
		}
		if (GUILayout.Button("Destroy Banner Ad", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width/2)))
		{
			// Destroy banner ad
			Adunion4Unity.Instance.destroyBannerAd();
		}
		GUILayout.EndHorizontal();
		GUILayout.Label("Link To", GUILayout.Height(Screen.height / 12));
		if (GUILayout.Button("Link to 'moregame'", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width)))
		{
			// Link to more game
			Adunion4Unity.Instance.linkTo(Adunion4Unity.LINK_TYPE_MOREGAME);
		}
		if (GUILayout.Button("Link to 'gamescore'", GUILayout.Height(Screen.height / 12), GUILayout.Width(Screen.width)))
		{
			// Link to google store for game score 
			Adunion4Unity.Instance.linkTo(Adunion4Unity.LINK_TYPE_GAMESCORE);
		}

		GUILayout.Label("video Ad: " + resultStr);

		#endif
	}
	
	void Update ()
	{
		#if UNITY_ANDROID
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
		{ 
			// Destroy cache
			Adunion4Unity.Instance.destroy();
			Application.Quit();
		}
		#endif
		
	}

	/*
	 *
	 *
	 *  result:    -1   the video is completed but is Skipped
	 *  result:     1   the video is completed
	 *  result:     2   the video is closed
	 *
	 */
	public void handleResult(string result){
		Debug.Log("video complete: " + result);
		if(result != null && !result.Trim().Equals("")){
			int t = int.Parse(result);
			if(t == 1){

				resultStr = " Completed And Not Skipped ";
			}else if(t == -1){
				resultStr = " Completed But Skipped ";
			}else if(t== 2){
				resultStr = " Closed ";
			}
		}
	}

}
