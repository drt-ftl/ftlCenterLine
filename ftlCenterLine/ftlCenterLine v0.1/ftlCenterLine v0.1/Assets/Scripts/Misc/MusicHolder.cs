using UnityEngine;
using System.Collections;

public class MusicHolder : MonoBehaviour 
{
	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
