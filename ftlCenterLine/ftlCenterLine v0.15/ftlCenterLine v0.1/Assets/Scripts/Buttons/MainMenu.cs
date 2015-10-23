using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;
using System;

public class MainMenu : ftlCenterLineManager 
{

	public void press()
	{
		GetComponent<GeneralButton> ().press ();
	}

	public void release()
	{
		GetComponent<GeneralButton> ().release ();
		foreach (var targetPiece in Targets)
			Destroy (targetPiece);
		Targets.Clear ();
		if (HomeBases.Count >= 1 && HomeBases[0] != null)
			Destroy (HomeBases[0]);
		HomeBases.Clear ();
		Hits = 0;
		SwitchToMenu ();
	}
	

}

