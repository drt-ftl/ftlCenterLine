using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class SoundEffectsButton : ftlCenterLineManager
{	
	private string soundEffectsStatus = "Off";
	
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
		if (SoundEffects)
		{
			soundEffectsStatus = "On";			
			GetComponent<SpriteRenderer>().color = drtFunction.ButtonPressed;
		}
		else
		{
			soundEffectsStatus = "Off";			
			GetComponent<SpriteRenderer>().color = drtFunction.ButtonColor;
		}
		var centroid = drt.transformToObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject);
		var GuiRect = (new Rect(centroid.x - 50, centroid.y - 50, 100, 100));
		GUI.Label(GuiRect, "SoundEffects: " + soundEffectsStatus, keyTextStyle);
	}
	public void press()
	{
		if (SoundEffects)
		{
			SoundEffects = false;		
			foreach (var source in GameObject.FindObjectsOfType<AudioSource>())
			{
				if (source.gameObject.name != "MusicHolder")
					source.volume = -256f;
			}
		}
		else
		{
			SoundEffects = true;
			foreach (var source in GameObject.FindObjectsOfType<AudioSource>())
			{
				if (source.gameObject.name != "MusicHolder")
					source.volume = 0.5f;
			}
		}
		if (SoundEffects)
			GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
	}
	public void release()
	{
	}
}
