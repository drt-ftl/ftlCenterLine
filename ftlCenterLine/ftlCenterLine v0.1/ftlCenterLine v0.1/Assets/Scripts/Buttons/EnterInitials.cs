using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;

public class EnterInitials : ftlCenterLineManager 
{
	public GameObject buttonObject;
	public GameObject keyboardKey;
	public GameObject keyboardSpaceBar;
	public GameObject keyboardEnterKey;
	public GameObject KeyboardBG;

	void OnEnable()
	{
		Button = buttonObject;
		KeyboardKey = keyboardKey;
		KeyboardEnterKey = keyboardEnterKey;
		KeyboardSpaceBar = keyboardSpaceBar;
	}

	public void press () 
	{
		if (GameObject.FindGameObjectsWithTag("LetterKey").Length == 0)
		{
			drt.AddStandardKeyboard (keyboardKey, keyboardSpaceBar, keyboardEnterKey, KeyboardBG, new Vector3(0f, -2.2f, -1f));
		}
		else
		{
			_keyboard.RemoveKeyboard();
		}
//		if (!SoundEffects)
//		{
//			foreach (var buttonToSilence in GameObject.FindObjectsOfType(typeof(GeneralButton)))
//			{
//				GameObject.Find(buttonToSilence.name).GetComponent<GeneralButton>().SoundEffects = false;
//			}
//		}
		foreach (var key in GameObject.FindGameObjectsWithTag("LetterKey"))
		{
			var position = key.transform.position;
			position.z -= 1.5f;
			key.GetComponentInChildren<Transform>().position = position;
			if (!SoundEffects)
				key.GetComponent<AudioSource>().volume = -256;
		}
		foreach (var key in GameObject.FindGameObjectsWithTag("OtherKey"))
		{
			if (!SoundEffects)
				key.GetComponent<AudioSource>().volume = -256;
		}
		var colliders = GameObject.Find ("CenterButtons").GetComponentsInChildren<BoxCollider>();
		foreach (var renderer in GameObject.Find ("CenterButtons").GetComponentsInChildren<SpriteRenderer>())
		{
			if (renderer.color == drtFunction.Invisible)
				renderer.color = drtFunction.ButtonColor;
			else
				renderer.color = drtFunction.Invisible;
		}
		foreach (var collider in colliders)
		{
			if (collider.enabled)
			{
				collider.enabled = false;
			}
			else
			{
				collider.enabled = true;
			}
		}
	}
	
	
	public void release () 
	{
		
	}
}
