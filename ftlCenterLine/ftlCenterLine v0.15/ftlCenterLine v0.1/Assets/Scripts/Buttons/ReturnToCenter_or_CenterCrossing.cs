using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class ReturnToCenter_or_CenterCrossing : ftlCenterLineManager 
{
	void Update()
	{
		name = ReturnToCenter_or_CenterCrossing;
	}
	public void press () 
	{
		if (ReturnToCenter_or_CenterCrossing == "Return To Center")
			ReturnToCenter_or_CenterCrossing = "Center Crossing";
		else
			ReturnToCenter_or_CenterCrossing = "Return To Center";
	}
	

	public void release () 
	{
	
	}
}
