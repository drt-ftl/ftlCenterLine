using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class MusicButton : ftlCenterLineManager
{	
	private string musicStatus = "Off";

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
		if (Music)
		{
			musicStatus = "On";			
			GetComponent<SpriteRenderer>().color = drtFunction.ButtonPressed;
		}
		else
		{
			musicStatus = "Off";			
			GetComponent<SpriteRenderer>().color = drtFunction.ButtonColor;
		}
		var centroid = drt.transformToObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject);
		var GuiRect = (new Rect(centroid.x - 50, centroid.y - 50, 100, 100));
		GUI.Label(GuiRect, "Music: " + musicStatus, keyTextStyle);
	}
	public void press()
	{
		if (Music)
		{
			Music = false;
			GetComponent<SpriteRenderer>().color = drtFunction.ButtonColor;
			GameObject.Find("MusicHolder").GetComponent<AudioSource>().volume = -256;
		}
		else
		{
			Music = true;
			GameObject.Find("MusicHolder").GetComponent<AudioSource>().volume = 0.5f;
			if (!GameObject.Find("MusicHolder").GetComponent<AudioSource>().isPlaying)
			{
				GameObject.Find("MusicHolder").GetComponent<AudioSource>().Play();
			}
		}
		if (SoundEffects)
			GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
	}
	public void release()
	{
	}
}
