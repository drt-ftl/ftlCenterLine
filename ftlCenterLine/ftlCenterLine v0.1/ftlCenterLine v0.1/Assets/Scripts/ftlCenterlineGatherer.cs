using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript;
using DRT_UnityLibrary;
using System.IO;

public class ftlCenterlineGatherer : ftlCenterLineManager 
{

	public GameObject box;
	public GameObject target;
	public GameObject homeBase;

	void OnEnable () 
	{
		homeTouches.Clear ();
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
			TouchManager.Instance.TouchesEnded += touchesEndedHandler;
			TouchManager.Instance.TouchesMoved += touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled += touchesCancelledHandler;
		}
		Target = target;
		//HomeBases.Add(homeBase);
		Targets.Clear ();
		Hits = 0;
		var path = drt.GetNewDirName ();
		tw = File.CreateText(path + "/SavedData.csv");	
		tw.WriteLine("Touch,Total Time, Round Time,X,Y,Target X,Target Y,Level,User: " + USER_INITIALS);
		SAVING = true;
		foreach (var _gameButton in GameObject.FindGameObjectsWithTag("GameButton"))
		{
			_gameButton.AddComponent<GeneralButton>();
		}
		SetPieces ();
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
		GameTimeElapsed = Time.realtimeSinceStartup - GameTimeStart;
	}
	void Update () 
	{

	}

	#region Event handlers
	
	private void touchesBeganHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (ftlTouches.ContainsKey(touch.Id)) return;
			var newFtlTouch = new ftlTouch();
			newFtlTouch.TouchObject = box;
			newFtlTouch.Add (touch, Time.realtimeSinceStartup);
			newFtlTouch.TouchObject.GetComponent<TouchObjectScript>().Id = touch.Id;
			ftlTouches.Add (touch.Id, newFtlTouch);
			var position = Camera.main.ScreenToWorldPoint(touch.Position);
			if (SAVING)
			{
				tw.WriteLine(touch.Id.ToString() 	// Writes out data for touch
				             + "," + GameTimeElapsed.ToString("f4")
				             + "," + (Time.realtimeSinceStartup - StartTime).ToString("f4")
				             + "," + position.x.ToString("f4")
				             + "," + position.y.ToString("f4")
				             + "," + TargetPosition.x.ToString("f4")
				             + "," + TargetPosition.y.ToString("f4")
				             + "," + CurrentLevel.ToString("f4"));
			}
		}
	}
	
	private void touchesMovedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (!ftlTouches.ContainsKey(touch.Id))
			{
				var newFtlTouch = new ftlTouch();
				newFtlTouch.TouchObject = box;
				newFtlTouch.Add (touch, Time.realtimeSinceStartup);
				newFtlTouch.TouchObject.GetComponent<TouchObjectScript>().Id = touch.Id;
				ftlTouches.Add (touch.Id, newFtlTouch);
				newFtlTouch.Active = true;
			}
			ftlTouches[touch.Id].Add (touch, Time.realtimeSinceStartup);
			if (SAVING)
			{
				var index = ftlTouches[touch.Id].ElementList.Count - 1;
				var element = ftlTouches[touch.Id].ElementList[index];
				tw.WriteLine(touch.Id.ToString() 	// Writes out data for touch
				             + "," + GameTimeElapsed.ToString("f4")
				             + "," + (Time.realtimeSinceStartup - StartTime).ToString("f4")
				             + "," + element.WorldPosition.x.ToString("f4")
				             + "," + element.WorldPosition.y.ToString("f4")				             
				             + "," + TargetPosition.x.ToString("f4")
				             + "," + TargetPosition.y.ToString("f4")
				             + "," + CurrentLevel.ToString("f4"));
			}
		}
	}
	
	private void touchesEndedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			ftlTouch dummy;
			if (!ftlTouches.TryGetValue(touch.Id, out dummy)) return;
			if (ftlTouches[touch.Id].LastButtonPressed != null)
			{
				if (ftlTouches[touch.Id].LastButtonPressed != null)
					ftlTouches[touch.Id].TouchObject.GetComponent<TouchObjectScript>().release(ftlTouches[touch.Id].LastButtonPressed);
			}
			Destroy (ftlTouches[touch.Id].TouchObject);
			ftlTouches[touch.Id].Active = false;
			//ftlTouches.Remove(touch.Id);
		}
	}
	
	private void touchesCancelledHandler(object sender, TouchEventArgs e)
	{
		touchesEndedHandler(sender, e);
	}
	
	#endregion

	public void SetPieces ()
	{
		var startPosition = new Vector3 (0f, -4f, 0f);
		var numberOfTargets = 5;
		var angleIncrement = 180.0f / (float) (numberOfTargets - 1);
		for (int i = 0; i < numberOfTargets; i++) 
		{
			var angle = i * Mathf.PI * angleIncrement / 180.0f;
			var vector = new Vector3 (-1f, 0f, 0f);
			vector.x = CurrentLevel * Mathf.Cos (angle);
			vector.y = CurrentLevel * (Mathf.Sin(angle)) - 2.0f;
			var newTarget = Instantiate (Target, vector * 2.0f, Quaternion.identity) as GameObject;
			newTarget.tag = "Target";
			Targets.Add (newTarget);
		}
		if (ReturnToCenter_or_CenterCrossing == "Center Crossing") 
		{
			var x = (int)(DateTime.Now.Millisecond / 100.0f);
			for (int i = 0; i < x; i++)
				rnd.Next (0, x);
			var target = Targets [rnd.Next (0, Targets.Count - 1)];
			target.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonPressed;
			TargetPosition = target.transform.position;
		}
		else
		{
			HomeBases.Clear ();
			var newHomeBase = Instantiate (homeBase, startPosition, Quaternion.identity) as GameObject;
			newHomeBase.tag = "Home Base";
			newHomeBase.name = "HomeBase";
			newHomeBase.GetComponent<SpriteRenderer> ().color = drtFunction.ButtonPressed;
			HomeBases.Add (newHomeBase);
		}
	}
}
