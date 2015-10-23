using UnityEngine;
using System.Collections;

public class GameDisplay : ftlCenterLineManager 
{
	private GUIStyle gdStyle;

	void OnEnable()
	{
		gdStyle = new GUIStyle();
		gdStyle.alignment = TextAnchor.UpperLeft;
		gdStyle.fontSize = 24;
		gdStyle.font = GameFont;
		gdStyle.normal.textColor = new Color (0f, 0f, 0f, 1f);
	}
	void OnGUI() 
	{	
		var centroid = drt.transformToObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject);
		var GuiRect = (new Rect(centroid.x - 50, centroid.y - 50, 100, 100));
		GUI.Label(GuiRect, "Player: " + USER_INITIALS + "\r\n" 
		          + "Score: " + Hits.ToString (), gdStyle);
	}
}
