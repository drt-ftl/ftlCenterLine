using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TouchScript;
using DRT_UnityLibrary;

public class ftlHomeGatherer : ftlCenterLineManager 
{

	public GameObject box;
	public Font gameFont;
	public GameObject mainMenuLevel;
	public GameObject gameLevel;
	public GameObject endLevel;
	
	void OnEnable () 
	{
		MainMenuLevel = mainMenuLevel;
		GameLevel = gameLevel;
		EndLevel = endLevel;
		Screen.fullScreen = true;
		drt.GameFont = gameFont;
		ftlTouches.Clear ();
		GameFont = gameFont;
		foreach (var _gameButton in GameObject.FindGameObjectsWithTag("GameButton"))
		{
			_gameButton.AddComponent<GeneralButton>();
		}
		foreach (var _button in GameObject.FindGameObjectsWithTag("Button"))
		{
			_button.AddComponent<GeneralButton>();
		}

		DRT_UnityLibrary.Keyboard.keyPressed += KeyPressedHandler;
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
			TouchManager.Instance.TouchesEnded += touchesEndedHandler;
			TouchManager.Instance.TouchesMoved += touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled += touchesCancelledHandler;
		}
		if (File.Exists(Application.dataPath + "/settings.csv"))
	    {
			var settingsReader = new StreamReader(File.OpenRead (Application.dataPath + "/settings.csv"));
			while (!settingsReader.EndOfStream)
			{
				var line = settingsReader.ReadLine();
				var settingsArray = line.Split(',');
				USER_INITIALS = settingsArray[0];
				StartingLevel = Convert.ToInt16(settingsArray[1]);
				NumberOfHitsPerLevel = Convert.ToInt16(settingsArray[2]);
				ReturnToCenter_or_CenterCrossing = settingsArray[3];
				Progressive_Or_OneLevel = settingsArray[4];
				if (GameObject.Find ("MusicHolder"))
				{
					if (settingsArray[5] == "True")
					{
						Music = true;
						GameObject.Find ("MusicHolder").GetComponent<AudioSource>().volume = 0.5f;
						if (!GameObject.Find("MusicHolder").GetComponent<AudioSource>().isPlaying)
						{
							GameObject.Find("MusicHolder").GetComponent<AudioSource>().Play();
						}
					} 
					else 
					{
						Music = false;
						GameObject.Find ("MusicHolder").GetComponent<AudioSource>().volume = -256;
					}
				}
				if (settingsArray[6] == "True")
					SoundEffects = true;
				else SoundEffects = false;
			}
		}
		if (SoundEffects)
		{
			foreach (var source in GameObject.FindObjectsOfType<AudioSource>())
			{
				if (source.gameObject.name != "MusicHolder")
					source.volume = 0.5f;
			}
			Camera.main.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioSource>().clip);
		}
		else
		{
			foreach (var source in GameObject.FindObjectsOfType<AudioSource>())
			{
				if (source.gameObject.name != "MusicHolder")
					source.volume = -256f;
			}
		}
	}

	private void OnDisable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
			TouchManager.Instance.TouchesEnded -= touchesEndedHandler;
			TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled -= touchesCancelledHandler;
		}
	}

	void OnGUI()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}

	#region Event handlers
	
	public void KeyPressedHandler(GameObject _key)
	{
		if (_key.name == "Enter")
		{
			var colliders = GameObject.Find ("CenterButtons").GetComponentsInChildren<BoxCollider>();
			foreach (var renderer in GameObject.Find ("CenterButtons").GetComponentsInChildren<SpriteRenderer>())
			{
				renderer.color = drtFunction.ButtonColor;
			}
			foreach (var collider in colliders)
			{
				collider.enabled = true;			
			}
		}
	}

	private void touchesBeganHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (homeTouches.ContainsKey(touch.Id)) return;
			var newFtlTouch = new ftlTouch();
			newFtlTouch.TouchObject = box;
			newFtlTouch.Add (touch, Time.realtimeSinceStartup);
			newFtlTouch.TouchObject.GetComponent<TouchObjectScript>().Id = touch.Id;
			homeTouches.Add (touch.Id, newFtlTouch);
		}
	}
	
	private void touchesMovedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (!homeTouches.ContainsKey(touch.Id))
			{
				var newFtlTouch = new ftlTouch();
				newFtlTouch.TouchObject = box;
				newFtlTouch.Add (touch, Time.realtimeSinceStartup);
				newFtlTouch.TouchObject.GetComponent<TouchObjectScript>().Id = touch.Id;
				homeTouches.Add (touch.Id, newFtlTouch);
				newFtlTouch.Active = true;
			}
			homeTouches[touch.Id].Add (touch, Time.realtimeSinceStartup);
		}
	}
	
	private void touchesEndedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			ftlTouch dummy;
			if (!homeTouches.TryGetValue(touch.Id, out dummy)) return;
			if (homeTouches[touch.Id].LastButtonPressed != null)
			{
				if (homeTouches[touch.Id].LastButtonPressed != null)
					homeTouches[touch.Id].TouchObject.GetComponent<TouchObjectScript>().release(homeTouches[touch.Id].LastButtonPressed);
			}
//			Destroy (homeTouches[touch.Id].TouchObject);
//			homeTouches[touch.Id].Active = false;
		}
	}
	
	private void touchesCancelledHandler(object sender, TouchEventArgs e)
	{
		touchesEndedHandler(sender, e);
	}
	
	#endregion
}
