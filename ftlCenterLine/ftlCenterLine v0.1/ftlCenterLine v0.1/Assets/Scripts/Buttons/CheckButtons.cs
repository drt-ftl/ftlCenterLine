using UnityEngine;
using System.Collections;
using DRT_UnityLibrary;
using System.Threading;

public class CheckButtons :ftlCenterLineManager
{
	private float buttonTime = 0;

	public void CheckAllButtonsHit(GameObject hit)
	{
		if (Time.realtimeSinceStartup - buttonTime < 0.5f)
			return;
		switch (hit.name) 
		{
			case "Quit":
				{
					hit.GetComponent<GeneralButton>().press ();
					break;
				}
			case "Start":
				{
					hit.GetComponent<GeneralButton>().press ();
					break;
				}

			case "Return To Center":
			{
				hit.GetComponent<GeneralButton>().press ();
				hit.GetComponent<ReturnToCenter_or_CenterCrossing>().press();
				break;
			}

			case "Center Crossing":
			{
				hit.GetComponent<GeneralButton>().press ();
				hit.GetComponent<ReturnToCenter_or_CenterCrossing>().press();
				break;
			}
			case "Progressive":
			{
				hit.GetComponent<GeneralButton>().press ();
				hit.GetComponent<Progressive_or_OneLevel>().press();
				break;
			}
			case "One Level":
			{
				hit.GetComponent<GeneralButton>().press ();
				hit.GetComponent<Progressive_or_OneLevel>().press();
				break;
			}
			case "Enter Initials":
			{
				hit.GetComponent<GeneralButton>().press ();
				hit.GetComponent<EnterInitials>().press();
				break;
			}
			case "StartingLevelUp":
			{
				hit.GetComponent<GeneralButton>().press ();
				if (StartingLevel < 4)
					StartingLevel++;
				break;
			}
			case "StartingLevelDown":
			{
				hit.GetComponent<GeneralButton>().press ();
				if (StartingLevel > 1)
					StartingLevel--;
				break;
			}
			case "HitsUp":
			{
				hit.GetComponent<GeneralButton>().press ();
				if (NumberOfHitsPerLevel < 21)
					NumberOfHitsPerLevel++;
				break;
			}
			case "HitsDown":
			{
				hit.GetComponent<GeneralButton>().press ();
				if (NumberOfHitsPerLevel > 3)
					NumberOfHitsPerLevel--;
				break;
			}
			default:
			{
				break;
			}
		}
		buttonTime = Time.realtimeSinceStartup;
	}

	public void CheckAllButtonsReleased(GameObject hit)
	{
		switch (hit.name) 
		{
		case "Quit":
		{
			hit.GetComponent<GeneralButton>().release ();
			Application.Quit ();
			break;
		}
		case "Start":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<StartButton>().release();
			break;
		}
		case "Return To Center":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<ReturnToCenter_or_CenterCrossing>().release();
			break;
		}
		case "Center Crossing":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<ReturnToCenter_or_CenterCrossing>().release();
			break;
		}
		case "Progressive":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<Progressive_or_OneLevel>().release();
			break;
		}
		case "One Level":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<Progressive_or_OneLevel>().release();
			break;
		}
		case "Enter Initials":
		{
			hit.GetComponent<GeneralButton>().release ();
			hit.GetComponent<EnterInitials>().release();
			break;
		}
		case "StartingLevelUp":
		{
			hit.GetComponent<GeneralButton>().release ();
			break;
		}
		case "StartingLevelDown":
		{
			hit.GetComponent<GeneralButton>().release ();
			break;
		}
		case "HitsUp":
		{
			hit.GetComponent<GeneralButton>().release ();
			break;
		}
		case "HitsDown":
		{
			hit.GetComponent<GeneralButton>().release ();
			break;
		}
		default:
		{
			break;
		}
		}
		buttonTime = Time.realtimeSinceStartup;
	}

}
