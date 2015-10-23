using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;
using System;
using System.IO;
using System.Threading;

public class StartButton : ftlCenterLineManager 
{

	public void release()
	{
		CurrentLevel = (float) StartingLevel;
		if (File.Exists(Application.dataPath + "/settings.csv"))
			File.Delete(Application.dataPath + "/settings.csv");
		Thread.Sleep (200);
		settingsWriter = File.CreateText (Application.dataPath + "/settings.csv");
		if (drt.GetInitials.ToCharArray().Length >= 1)
			USER_INITIALS = drt.GetInitials;
		settingsWriter.WriteLine (USER_INITIALS +
			"," + StartingLevel.ToString () +
			"," + NumberOfHitsPerLevel.ToString () +
			"," + ReturnToCenter_or_CenterCrossing +
			"," + Progressive_Or_OneLevel +
			"," + Music.ToString() +
			"," + SoundEffects.ToString ());
		settingsWriter.Close ();
		SAVING = true;
		StartTime = Time.realtimeSinceStartup;
		TotalHits = 0;
		GameTimeStart = Time.realtimeSinceStartup;
		Application.LoadLevel ("Game");
	}
}
