using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;
using System;

public class Continue : ftlCenterLineManager 
{
	
	public void press()
	{
		GetComponent<GeneralButton> ().press ();
	}
	
	public void release()
	{
		GetComponent<GeneralButton> ().release ();
		if (Progressive_Or_OneLevel == "Progressive") 
		{
			if (CurrentLevel < 4 && CurrentLevel < StartingLevel + 1.5f)
				CurrentLevel += 0.2f;
			else
				CurrentLevel = StartingLevel;
		}
		StartTime = Time.realtimeSinceStartup;
		SwitchToPlay ();
	}
	
	
}