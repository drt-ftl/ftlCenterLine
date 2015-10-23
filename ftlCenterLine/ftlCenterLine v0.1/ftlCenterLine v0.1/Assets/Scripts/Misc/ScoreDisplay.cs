using UnityEngine;
using System.Collections;

public class ScoreDisplay : ftlCenterLineManager 
{
	private GUIStyle gdStyle;
	
	void OnEnable()
	{
		gdStyle = new GUIStyle();
		gdStyle.alignment = TextAnchor.MiddleCenter;
		gdStyle.font = Camera.main.GetComponent<ftlHomeGatherer> ().gameFont;
		gdStyle.fontSize = 36;
		gdStyle.normal.textColor = new Color (0f, 0f, 0f, 1f);
	}
	void OnGUI() 
	{	
		var centroid = drt.transformToObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject);
		var GuiRect = (new Rect(centroid.x - 50, centroid.y - 50, 100, 100));
		GUI.Label(GuiRect, "Player: " + USER_INITIALS + "\r\n" 
		          + "Score: " + TotalHits.ToString () + "\r\n"
		          + "Time/Hit: " + (time/TotalHits).ToString ("f2") + "\r\n"
		          + "Time: " + time.ToString ("f2") + "sec" + "\r\n"
		          + "Total Time: " + TotalTime.ToString ("f2") + "sec", gdStyle);
	}
}
