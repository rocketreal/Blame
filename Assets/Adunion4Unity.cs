using UnityEngine;
using System.Collections;

public class Adunion4Unity
{
	//Interstitial Ad Type
	public const string IAD_TYPE_GAMESTART = "gamestart";
	public const string IAD_TYPE_GAMEPAUSE = "gamepause";
	public const string IAD_TYPE_GAMEGIFT = "gamegift";
	public const string IAD_TYPE_GAMEEXIT = "gameexit";
	// Banner Ad Position
	public const int BAD_POS_TOP_LEFT = 0;
	public const int BAD_POS_TOP_CENTER = 1;
	public const int BAD_POS_TOP_RIGHT = 2;
	public const int BAD_POS_CENTER_LEFT = 3;
	public const int BAD_POS_CENTER_CENTER = 4;
	public const int BAD_POS_CENTER_RIGHT = 5;
	public const int BAD_POS_BOTTOM_LEFT = 6;
	public const int BAD_POS_BOTTOM_CENTER = 7;
	public const int BAD_POS_BOTTOM_RIGHT = 8;
	//LinkTo Type
	public const string LINK_TYPE_MOREGAME = "moregame";
	public const string LINK_TYPE_GAMESCORE = "gamescore";
	//Singleton
	private static Adunion4Unity mInstance = new Adunion4Unity ();
	private AndroidJavaClass mAdUnionCls;
	private AndroidJavaObject mActivity;

	public static Adunion4Unity Instance {
		get {
			mInstance.init ();
			return mInstance;
		}
	}
	
	private void init ()
	{
		if (null == mAdUnionCls) {
			mAdUnionCls = new AndroidJavaClass ("com.ltad.core.Adunion4Unity");
		}
		
		if (null == mActivity) {
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				mActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			}
		}
	}

	public void setHandleName(string name)
	{
		mAdUnionCls.CallStatic("setHandleName",name);
	}

	public void preloadInterstitialAd(){
		mAdUnionCls.CallStatic("preloadInterstitialAd", mActivity);
	}
	/**
	     * Call this method can show a interstitial ad.
	     * param point is a integer num, point >0
	     * 
	     * For example: Adunion4Unity.Instance.showInterstitial(1);
	     * */
	public void showInterstitial(string point){
		mAdUnionCls.CallStatic("showInterstitial", mActivity, point);
	}

	/**
	     * Call this method can show a interstitial ad.
	     * Display advertising by calling an interface to pass different parameters to achieve:
	     * 
	     * @parameters type
		 * Adunion4Unity.IAD_TYPE_GAMESTART ---- Display ads at the beginning of the game
		 * Adunion4Unity.IAD_TYPE_GAMEPAUSE ---- Display ads on the game is paused
		 * Adunion4Unity.IAD_TYPE_GAMEGIFT  ---- Display ads on the end of the checkpoint or character death
		 * Adunion4Unity.IAD_TYPE_GAMEEXIT  ---- Display ads at the end of the game
		 * 
		 * For example: Adunion4Unity.Instance.showInterstitialAd(Adunion4Unity.IAD_TYPE_GAMESTART);
	     */
	public void showInterstitialAd (string type)
	{
		mAdUnionCls.CallStatic ("showInterstitialAd", mActivity, type);
	}

	/**
		* Call this method can stop loading a interstitial ad.
		* ad is not loaded completely, the calling is valid
	*/
	public void stopLoadingInterstitialAd(){
		mAdUnionCls.CallStatic("stopLoadingInterstitialAd", mActivity);
	}

	public void preloadBannerAd(){
		mAdUnionCls.CallStatic("preloadBannerAd", mActivity);
	}

	/**
	     * Call this method can show a banner ad
	     * 
	     * @parameters position
	     * -------------------------------------------------------------------------------------------------------------------
	     * |								    |									   |									  |
	     * | Adunion4Unity.BAD_POS_TOP_LEFT     | Adunion4Unity.BAD_POS_TOP_CENTER     |   Adunion4Unity.BAD_POS_TOP_RIGHT    |
	     * |								    |									   |									  |
	     * -------------------------------------------------------------------------------------------------------------------|
	     * |								    |									   |									  |
	     * | Adunion4Unity.BAD_POS_CENTER_LEFT  | Adunion4Unity.BAD_POS_CENTER_CENTER  |  Adunion4Unity.BAD_POS_CENTER_RIGHT  |
	     * |								    |									   |									  |
	     * -------------------------------------------------------------------------------------------------------------------|
	     * |								    |									   |									  |
	     * | Adunion4Unity.BAD_POS_BOTTOM_LEFT  | Adunion4Unity.BAD_POS_BOTTOM_CENTER  |  Adunion4Unity.BAD_POS_BOTTOM_RIGHT  |
	     * |								    |									   |									  |
	     * -------------------------------------------------------------------------------------------------------------------
	     * 
	     * For example: Adunion4Unity.Instance.showBannerAd(Adunion4Unity.BAD_POS_TOP_LEFT);
	     */
	public void showBannerAd (int position)
	{
		mAdUnionCls.CallStatic ("showBannerAd", mActivity, position);
	}
	
	/**
		 * Call this method can close banner ad
	     */
	public void closeBannerAd ()
	{
		mAdUnionCls.CallStatic ("closeBannerAd", mActivity);
	}

	/**
		 * Call this method can destroy banner ad
	     */
	public void destroyBannerAd ()
	{
		mAdUnionCls.CallStatic ("destroyBannerAd", mActivity);
	}
	
	
	/**
		 * Link to the assist page
		 * 
		 * @parameters type
		 * Adunion4Unity.LINK_TYPE_MOREGAME ---- Access to more game link
		 * Adunion4Unity.LINK_TYPE_GAMESCORE ---- Open google stroe for game score
		 * 
		 * For example: Adunion4Unity.Instance.linkTo(Adunion4Unity.LINK_TYPE_MOREGAME);
		 */
	public void linkTo (string type)
	{
		mAdUnionCls.CallStatic ("linkTo", mActivity, type);
	}
	
	/**
	     * Destroy method
	     */
	public void destroy ()
	{
		mAdUnionCls.CallStatic ("destroy", mActivity);
		mAdUnionCls = null;
		mActivity = null;
	}
	
}
