using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using DRT_UnityLibrary;

public class TouchObjectScript : ftlCenterLineManager
{
	private GameObject grabbedObject;
	private Vector3 offset;
	private bool dragging = false;
	private Vector3 grabbedExtents = Vector3.zero;
	private int _id;
	private CheckButtons check = new CheckButtons();
	private float buttonTime = 0;

	void OnEnable ()
	{
	}

	void OnGUI()
	{
		if (dragging)
		{
			grabbedObject.transform.position = transform.position + offset;
		}
	}

	public int Id
	{
		get { return _id;}
		set { _id = value;}
	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.GetComponent<SpriteRenderer> ().color == drtFunction.Invisible)
			return;
		press (hit.gameObject);
	}

	void OnTriggerExit (Collider hit)
	{
		if (hit.GetComponent<SpriteRenderer> ().color == drtFunction.Invisible)
			return;
		release (hit.gameObject);
	}

	public void press (GameObject hit)
	{
		if (hit.tag == "Grab" && !dragging) {
			grabbedObject = hit.gameObject;
			offset = hit.transform.position - transform.position;
			grabbedExtents = hit.transform.GetComponent<Collider> ().bounds.extents;
			dragging = true;
		}
		else if (hit.tag == "Button")
		{
			check.CheckAllButtonsHit(hit.gameObject);
		}
		else if (hit.tag == "LetterKey")
		{
			if (Time.realtimeSinceStartup - buttonTime < 0.5f)
				return;
			buttonTime = Time.realtimeSinceStartup;
			hit.GetComponent<LetterKey>().press();
		}
		else if (hit.tag == "OtherKey")
		{
			if (Time.realtimeSinceStartup - buttonTime < 0.5f)
				return;
			buttonTime = Time.realtimeSinceStartup;
			hit.GetComponent<OtherKey>().press();
		}
		else if (hit.tag == "Music")
		{
			hit.GetComponent<MusicButton>().press();
		}
		else if (hit.tag == "SoundEffects")
		{
			hit.GetComponent<SoundEffectsButton>().press();
		}
		else if (hit.tag == "GameButton")
		{
			if (hit.name == "Quit")
				check.CheckAllButtonsHit(hit.gameObject);
			else if (hit.name == "Main Menu")
				hit.GetComponent<MainMenu>().press();
			else if (hit.name == "Continue")
				hit.GetComponent<Continue>().press();
		}
		else if (hit.tag == "Target")
		{
			if (hit.GetComponent<SpriteRenderer>().color == drtFunction.ButtonPressed)
			{
				if (SoundEffects)
					hit.GetComponent<AudioSource>().PlayOneShot(hit.GetComponent<AudioSource>().clip);
				hit.GetComponent<SpriteRenderer>().color = drtFunction.ButtonColor;
				if (ReturnToCenter_or_CenterCrossing == "Return To Center")
				{
					if (HomeBases.Count >= 1 && HomeBases[0] != null)
					{
						HomeBases[0].GetComponent<SpriteRenderer>().color = drtFunction.ButtonPressed;
						TargetPosition = HomeBases[0].transform.position;
					}
				}
				else
				{
					var x = (int)(DateTime.Now.Millisecond / 100.0f);
					for (int i = 0; i < x; i++)
						rnd.Next (0, x);
					var index = Targets.IndexOf(hit);
					var rand = rnd.Next(0, Targets.Count);
					if (index == 2)
					{
						if (x >= 5)
							rand = 4;
						else
							rand = 0;
					}
					else if (index < 2)
					{
						rand = rnd.Next(2, Targets.Count);
					}
					else if (index > 2)
					{
						rand = rnd.Next(0, 2);
					}
					var target = Targets[rand];
					target.GetComponent<SpriteRenderer>().color = drtFunction.ButtonPressed;
					TargetPosition = target.transform.position;
				}
				Hits++;
				TotalHits++;

				if (NumberOfHitsPerLevel != 21 && Hits >= NumberOfHitsPerLevel)
				{
					if (SoundEffects)
						Camera.main.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioSource>().clip);
					Thread.Sleep (500);
					time = Time.realtimeSinceStartup - StartTime;
					TotalTime += time;
					SwitchToEnd();
				}
			}
		}
		else if (hit.tag == "Home Base")
		{
			if (hit.GetComponent<SpriteRenderer>().color == drtFunction.ButtonPressed)
			{
				if (SoundEffects)
					hit.GetComponent<AudioSource>().PlayOneShot(hit.GetComponent<AudioSource>().clip);
				hit.GetComponent<SpriteRenderer>().color = drtFunction.ButtonColor;
				var x = (int)(DateTime.Now.Millisecond / 100.0f);
				for (int i = 0; i < x; i++)
					rnd.Next (0, x);
				var rand = rnd.Next(0, Targets.Count);
				var target = Targets[rand];
				target.GetComponent<SpriteRenderer>().color = drtFunction.ButtonPressed;
				TargetPosition = target.transform.position;
				Hits++;
				TotalHits++;
				if (NumberOfHitsPerLevel != 21 && Hits >= NumberOfHitsPerLevel)
				{
					if (SoundEffects)
						Camera.main.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioSource>().clip);
					Thread.Sleep (500);
					time = Time.realtimeSinceStartup - StartTime;
					TotalTime += time;
					SwitchToEnd();
				}
			}
		}
		if (ftlTouches.ContainsKey (Id))
			ftlTouches[Id].LastButtonPressed = hit.gameObject;
		if (homeTouches.ContainsKey (Id))
			homeTouches[Id].LastButtonPressed = hit.gameObject;
	}


	public void release (GameObject hit)
	{
		if (hit.tag == "Button")
		{
			var check = new CheckButtons();
			check.CheckAllButtonsReleased(hit.gameObject);
		}
		else if (hit.tag == "LetterKey")
		{
			buttonTime = Time.realtimeSinceStartup;
			hit.GetComponent<LetterKey>().release();
		}
		else if (hit.tag == "OtherKey")
		{
			buttonTime = Time.realtimeSinceStartup;
			hit.GetComponent<OtherKey>().release();
		}
		else if (hit.tag == "GameButton")
		{
			if (hit.name == "Quit")
			{
				check.CheckAllButtonsReleased(hit.gameObject);
				tw.Close ();
				Application.Quit ();
			}
			else if (hit.name == "Main Menu")
			{
				hit.GetComponent<MainMenu>().release();
				tw.Close ();
			}
			if (hit.name == "Replay")
			{
				check.CheckAllButtonsReleased(hit.gameObject);
				tw.Close ();
			}
			else if (hit.name == "Continue")
				hit.GetComponent<Continue>().release();
			SAVING = false;
		}
	}

	public GameObject GetGrabbedObject ()
	{
		return grabbedObject;
	}
}
