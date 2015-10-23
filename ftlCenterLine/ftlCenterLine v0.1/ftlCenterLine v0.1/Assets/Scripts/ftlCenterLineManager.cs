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

	void Awake () 
	{
		drt = Camera.main.gameObject.AddComponent<drtFunction> ();
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
		foreach (var button in GameObject.FindGameObjectsWithTag("Button")) {
			button.GetComponent<SpriteRenderer> ().color = drtFunction.Invisible;
		}
		foreach (var buttonBase in GameObject.FindGameObjectsWithTag("ButtonBase")) {
			buttonBase.GetComponent<SpriteRenderer> ().color = drtFunction.Invisible;
		}
		foreach (var musicButton in GameObject.FindGameObjectsWithTag("Music")) {
			musicButton.GetComponent<SpriteRenderer> ().color = drtFunction.Invisible;
		}
		foreach (var soundEffectsButton in GameObject.FindGameObjectsWithTag("SoundEffects")) {
			soundEffectsButton.GetComponent<SpriteRenderer> ().color = drtFunction.Invisible;
		}
		foreach (var gameButton in GameObject.FindGameObjectsWithTag("GameButton"))
		{
			gameButton.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonColor;
		}
	}

	public void SwitchToMenu ()
	{
		foreach (var button in GameObject.FindGameObjectsWithTag("Button")) {
			button.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonColor;
		}
		foreach (var buttonBase in GameObject.FindGameObjectsWithTag("ButtonBase")) {
			buttonBase.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonColor;
		}
		foreach (var musicButton in GameObject.FindGameObjectsWithTag("Music")) {
			musicButton.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonColor;
		}
		foreach (var soundEffectsButton in GameObject.FindGameObjectsWithTag("SoundEffects")) {
			soundEffectsButton.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonColor;
		}
		foreach (var gameButton in GameObject.FindGameObjectsWithTag("GameButton"))
		{
			gameButton.GetComponent<SpriteRenderer> ().color = drtFunction.Invisible;
		}
	}
}
