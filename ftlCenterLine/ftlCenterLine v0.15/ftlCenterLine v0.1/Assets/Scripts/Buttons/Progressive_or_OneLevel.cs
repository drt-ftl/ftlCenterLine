using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class Progressive_or_OneLevel : ftlCenterLineManager 
{
	void Update()
	{
		name = Progressive_Or_OneLevel;
	}
	public void press () 
	{
		if (Progressive_Or_OneLevel == "Progressive")
			Progressive_Or_OneLevel = "One Level";
		else
			Progressive_Or_OneLevel = "Progressive";
	}
	

	public void release () 
	{
	
	}
}
