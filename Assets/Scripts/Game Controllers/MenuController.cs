using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System.Collections.Generic;
//using UnityEngine.Analytics;

public class MenuController : MonoBehaviour
{

    public static string startScreenName = "Start Screen",
        manipulationSelectScreenName = "Manipulation Select Screen",
        destructionSelectScreenName = "Destruction Select Screen",
        constructionSelectScreenName = "Construction Select Screen",
        helpScreenName = "Help Screen",
        aboutScreenName = "About Screen",
        quitScreenName = "Quit Screen",
        websiteText = "http://divf.eng.cam.ac.uk/gam2eng/Main/WebHome";

    public AudioClip soundMouseEnter, soundMouseExit;

    public static bool inMenu;

    public string finalQuitURL;

    void Start()
    {
        Cursor.visible = true;
        inMenu = (Application.loadedLevelName.Contains("Screen"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.loadedLevelName == startScreenName)
            {
                GoToQuitScreen();
            }
            else if (Application.loadedLevelName.Contains("Screen"))
            {
                GoToStartScreen();
            }
            else
            {
//				Debug.Log ("quitGame - levelName: " + Application.loadedLevelName);
				GoogleAnalytics.Client.SendTimedEventHit("gameFlow", "quitGame", Application.loadedLevelName, Time.timeSinceLevelLoad);
				
//				Analytics.CustomEvent("quitGame", new Dictionary<string, object>
//				                      {
//					{ "levelName", Application.loadedLevelName},
//					{ "levelTime", Time.timeSinceLevelLoad},
//				});				
				
				if (Application.loadedLevelName.Contains("Manip"))//Manipulation
                {
                    GoToManipulationSelectScreen();
                }
                else if (Application.loadedLevelName.Contains("Destr"))//Destruction
                {
                    GoToDestructionSelectScreen();
                }
                else if (Application.loadedLevelName.Contains("Const"))//Construction
                {
                    GoToConstructionSelectScreen();
                }
                else
                {
                    GoToStartScreen();
                }
            }
        }
    }

    public static void LoadLevel(string levelName)
    {
//		Debug.Log ("levelLoad - levelName: " + levelName);
		GoogleAnalytics.Client.SendEventHit("gameFlow", "levelLoad", levelName);
//		
//		Analytics.CustomEvent("levelLoad", new Dictionary<string, object>
//		{
//			{ "levelName", levelName},
//		});	
		
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            inMenu = (levelName.Contains("Screen"));
            Application.LoadLevel(levelName);
        }
        else
        {
            Debug.LogWarning("Attempted to load non-existant level: " + levelName + " (it may need to be added to the build settings)");
        }
    }

    public void GoToStartScreen()
    {

        LoadLevel(startScreenName);
    }
    public void GoToManipulationSelectScreen()
    {
        LoadLevel(manipulationSelectScreenName);
    }
    public void GoToDestructionSelectScreen()
    {
        LoadLevel(destructionSelectScreenName);
    }
    public void GoToConstructionSelectScreen()
    {
        LoadLevel(constructionSelectScreenName);
    }
    public void GoToHelpScreen()
    {
        LoadLevel(helpScreenName);
    }
    public void GoToAboutScreen()
    {
        LoadLevel(aboutScreenName);
    }
    public void GoToQuitScreen()
    {
        LoadLevel(quitScreenName);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		if (finalQuitURL != ""){
			Application.OpenURL(finalQuitURL);
		}
#else
		if (finalQuitURL != ""){
			Application.OpenURL(finalQuitURL);
		}
		Application.Quit();
#endif
    }

    public void StartLevel(GameObject callingButton)
    {
        string levelName = callingButton.GetComponentInChildren<Text>().text;
//		Debug.Log ("startGame - levelName: " + levelName);
		GoogleAnalytics.Client.SendEventHit("gameFlow", "startGame", Application.loadedLevelName);
		
//		Analytics.CustomEvent("startGame", new Dictionary<string, object>
//		{
//			{ "levelName", Application.loadedLevelName},
//		});		
				
		LoadLevel(levelName);
		
		
    }
    public void CopyWebsiteText()
    {
        //To be filled in at some point maybe?
    }

    public void MouseEnter()
    {
        if (soundMouseEnter != null)
        {
            AudioSource.PlayClipAtPoint(soundMouseEnter, transform.position, 0.2f);
        }
        return;
    }
    public void MouseExit()
    {
        if (soundMouseExit != null)
        {
            AudioSource.PlayClipAtPoint(soundMouseExit, transform.position, 0.2f);
        }
    }
}
