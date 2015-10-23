using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		MovieTexture movie = (MovieTexture)GetComponent<Renderer> ().material.mainTexture;
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (!movie.isPlaying)
				movie.Play ();
			else
				Destroy(gameObject);
		}
	}
}
