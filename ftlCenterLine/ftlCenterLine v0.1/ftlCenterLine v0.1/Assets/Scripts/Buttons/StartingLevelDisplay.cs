﻿using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class StartingLevelDisplay : ftlCenterLineManager
{	
	void OnEnable()
	{
		keyTextStyle = new GUIStyle();
		keyTextStyle.alignment = TextAnchor.MiddleCenter;
		keyTextStyle.fontSize = 14;
		keyTextStyle.font = Camera.main.GetComponent<ftlHomeGatherer> ().gameFont;
		keyTextStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
	}

	void OnGUI()
	{
		if (GetComponent<SpriteRenderer> ().color == drtFunction.Invisible)
			return;
		var centroid = drt.transformToObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject);
		var GuiRect = (new Rect(centroid.x - 50, centroid.y - 50, 100, 100));
		GUI.Label(GuiRect, "Starting Level: " + StartingLevel.ToString(), keyTextStyle);
	}
}
