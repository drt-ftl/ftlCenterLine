using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DRT_UnityLibrary;

public class ftlCenterLineManager : MonoBehaviour 
{
	public static Dictionary<int, ftlTouch> ftlTouches = new Dictionary<int, ftlTouch>();
	public static Dictionary <int, ftlTouch> homeTouches = new Dictionary<int, ftlTouch>();
	public static List<GameObject> Targets = new List<GameObject>();
	public static GUIStyle keyTextStyle;
	public static Vector3 BL;
	public static Vector3 UR;
	public static DRT_UnityLibrary.drtFunction drt;
	public static DRT_UnityLibrary.Keyboard _keyboard;

	public static string USER_INITIALS = "";
	public static bool Playing = false;
	public static int StartingLevel = 1;
	public static int NumberOfHitsPerLevel = 3;
	public static string Progressive_Or_OneLevel = "Progressive";
	public static string ReturnToCenter_or_CenterCrossing = "Return To Center";
	public static bool SoundEffects = true;
	public static bool Music = false;

	public static GameObject Button;
	public static GameObject KeyboardKey;
	public static GameObject KeyboardSpaceBar;
	public static GameObject KeyboardEnterKey;

	public static GameObject Target;
	public static List<GameObject> HomeBases = new List<GameObject> ();
	public static System.Random rnd = new System.Random();
	public static int Hits = 0;
	public static int TotalHits = 0;
	public static TextWriter tw;
	public static TextWriter settingsWriter;
	public static bool SAVING = false;
	public static float CurrentLevel = 1;
	public static Vector3 TargetPosition = Vector2.zero;
	public static float StartTime = 0;
	public static float time = 0;
	public static float TotalTime = 0;
	public static float GameTimeElapsed = 0;
	public static float GameTimeStart = 0;
	public static Font GameFont;
	public enum GameMode {MainMenu, Game, End};
	public static GameMode MODE;
	public static GameObject MainMenuLevel;
	public static GameObject GameLevel;
	public static GameObject EndLevel;

	void Awake () 
	{
		MODE = GameMode.MainMenu;
		drt = Camera.main.GetComponent<drtFunction> ();
		keyTextStyle = new GUIStyle();
		keyTextStyle.alignment = TextAnchor.MiddleCenter;
		keyTextStyle.fontSize = 14;
		if (Camera.main.GetComponent<ftlHomeGatherer>() != null)
			GameFont = Camera.main.GetComponent<ftlHomeGatherer> ().gameFont;
		keyTextStyle.font = GameFont;
		keyTextStyle.normal.textColor = new Color (0f, 0f, 0f, 1f);
		_keyboard = drt.GetKeyboard;
		drt.ButtonsInitialize ();
	}

	void Start()
	{
		drt.ButtonsInitialize ();
	}

	public void SwitchToPlay ()
	{
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject")) 
		{
			if (tO.GetComponent<TouchObjectScript> () != null)
				tO.GetComponent<TouchObjectScript> ().enabled = false;
		}
		Camera.main.GetComponent<ftlCenterlineGatherer> ().enabled = false;
		Camera.main.GetComponent<ftlHomeGatherer> ().enabled = false;
		System.Threading.Thread.Sleep (20);
		homeTouches.Clear ();
		ftlTouches.Clear ();
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject"))
			Destroy (tO);
		MainMenuLevel.SetActive (false);
		EndLevel.SetActive (false);
		GameLevel.SetActive (true);
		System.Threading.Thread.Sleep (20);
		Camera.main.GetComponent<ftlCenterlineGatherer> ().enabled = true;
		MODE = GameMode.Game;
	}

	public void SwitchToMenu ()
	{
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject"))
		{
			if (tO.GetComponent<TouchObjectScript> () != null)
				tO.GetComponent<TouchObjectScript> ().enabled = false;
		}
		Camera.main.GetComponent<ftlHomeGatherer> ().enabled = false;
		Camera.main.GetComponent<ftlCenterlineGatherer> ().enabled = false;
		System.Threading.Thread.Sleep (20);
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject"))
			Destroy (tO);
		homeTouches.Clear ();
		ftlTouches.Clear ();
		if (GameObject.Find ("EndLevel"))
			GameObject.Find ("EndLevel").SetActive (false);
		if (GameObject.Find ("GameLevel"))
			GameObject.Find ("GameLevel").SetActive (false);
		if (GameObject.Find ("MainMenuLevel"))
			GameObject.Find ("MainMenuLevel").SetActive (true);
		MainMenuLevel.SetActive (true);
		EndLevel.SetActive (false);
		GameLevel.SetActive (false);
		Camera.main.GetComponent<ftlHomeGatherer> ().enabled = true;
		MODE = GameMode.MainMenu;
	}

	public void SwitchToEnd ()
	{
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject"))
		{
			if (tO.GetComponent<TouchObjectScript> () != null)
				tO.GetComponent<TouchObjectScript> ().enabled = false;
		}
		foreach (var target in GameObject.FindGameObjectsWithTag("Target"))
			Destroy (target);
		foreach (var homeBase in GameObject.FindGameObjectsWithTag("Home Base"))
			Destroy (homeBase);
		Camera.main.GetComponent<ftlCenterlineGatherer> ().enabled = false;
		Camera.main.GetComponent<ftlHomeGatherer> ().enabled = false;
		System.Threading.Thread.Sleep (20);
		foreach (var tO in GameObject.FindGameObjectsWithTag("TouchObject"))
			Destroy (tO);
		homeTouches.Clear ();
		ftlTouches.Clear ();
		MainMenuLevel.SetActive (false);
		EndLevel.SetActive (true);
		GameLevel.SetActive (false);
		Camera.main.GetComponent<ftlHomeGatherer> ().enabled = true;
		MODE = GameMode.End;
		Hits = 0;
	}
}
