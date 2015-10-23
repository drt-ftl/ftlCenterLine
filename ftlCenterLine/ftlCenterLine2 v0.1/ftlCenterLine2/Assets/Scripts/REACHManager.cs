using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ftlUnityBuddy;

public class REACHManager : MonoBehaviour 
{
	public enum Mode {Intro, Play, End};
	public static Mode mode;
	public static int currentNumber;
	public static int lastNumber;
	private SoundBoard bgSoundBoard;
	private AudioSource bgAudioSource;
	public static float clipLength = 0;
	private bool playing = false;
	private static int score;
	private static float lastTimeHit = 0;
	private static float lastTime = 0;
	private static float totalTime = 0;
	private static float averageTime = 0;

	void Awake ()
	{
		mode = Mode.Intro;
		currentNumber = UnityEngine.Random.Range (1, 20);
		bgSoundBoard = GameObject.Find ("BG").GetComponent<SoundBoard> ();
		bgAudioSource = GameObject.Find ("BG").GetComponent<AudioSource> ();
		for (int i = 1; i <= 19; i++)
		{
			var dim = GameObject.Find (i.ToString ());
			dim.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		}
	}

	void StartGame () 
	{
		lastTimeHit = Time.realtimeSinceStartup;
		mode = Mode.Play;
		var clip = bgSoundBoard.numberClips[currentNumber - 1];
		clipLength = clip.length;
		bgAudioSource.PlayOneShot (clip);
		lastNumber = currentNumber;
		var lightUp = GameObject.Find (currentNumber.ToString ());
		lightUp.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		Gatherer.ftlTouchHit += hit;
	}
	void OnGUI () 
	{
		switch (mode)
		{
		case Mode.Intro:
			if (GUI.Button(new Rect (70, Screen.height - 50, 100, 30), "START"))
		    {
				Click ();
				StartGame();
			}
			break;
		case Mode.Play:
			if (GUI.Button(new Rect (70, Screen.height - 90, 100, 30), "RESTART"))
			{
				Click ();
				Restart();
			}
			if (GUI.Button(new Rect (70, Screen.height - 50, 100, 30), "QUIT"))
			{
				Click();
				Application.Quit();
			}
			var scoreRect = new Rect (Screen.width - 200, Screen.height - 130, 150, 30);
			GUI.color = new Color (0f,0f,0f,1f);
			GUI.Label (scoreRect, "Score: " + score.ToString ());
			scoreRect.y += 40;
			GUI.Label (scoreRect, "Last Time: " + lastTime.ToString ("f2"));
			scoreRect.y += 40;
			GUI.Label (scoreRect, "Average Time: " + averageTime.ToString ("f2"));
			break;
		default:
			break;
		}
	}

	public void GenerateNextNumber ()
	{
		currentNumber = UnityEngine.Random.Range (1, 20);
		if (currentNumber == lastNumber)
		{
			GenerateNextNumber ();
			return;
		}
		var clip = bgSoundBoard.numberClips[currentNumber - 1];
		clipLength = clip.length;
		bgAudioSource.PlayOneShot (clip);
		var dim = GameObject.Find (lastNumber.ToString ());
		dim.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		lastNumber = currentNumber;
		var lightUp = GameObject.Find (currentNumber.ToString ());
		lightUp.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		lastTime = Time.realtimeSinceStartup - lastTimeHit;
		score++;
		totalTime += lastTime;
		averageTime = totalTime / score;
		lastTimeHit = Time.realtimeSinceStartup;
		Click ();
	}

	void Restart ()
	{
		score = 0;
		lastTime = 0;
		averageTime = 0;
		totalTime = 0;
		lastTimeHit = Time.realtimeSinceStartup;
		currentNumber = UnityEngine.Random.Range (1, 20);
		for (int i = 1; i <= 19; i++)
		{
			var dim = GameObject.Find (i.ToString ());
			dim.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
		}
		StartGame();
	}

	void Click()
	{
		var clickSource = Camera.main.GetComponent<AudioSource> ();
		var click = clickSource.clip;
		clickSource.PlayOneShot (click);
	}

	void hit (GameObject _hit, ftlTouch _touch)
	{
		try
		{
			var thisNumber = Convert.ToInt16(_hit.transform.root.name);
			if (thisNumber == currentNumber)
			{
				GenerateNextNumber();
			}
		}
		catch{}
	}
}
