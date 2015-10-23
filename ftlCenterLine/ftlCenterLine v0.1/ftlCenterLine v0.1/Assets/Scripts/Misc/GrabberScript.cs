using UnityEngine;
using System.Collections;

public class GrabberScript : ftlCenterLineManager
{
	private GameObject grabbedObject;
	private Vector3 offset;
	private bool dragging = false;
	private Vector3 grabbedExtents = Vector3.zero;

	void OnGUI()
	{
		if (dragging)
		{
			grabbedObject.transform.position = transform.position + offset;
		}
	}
	void OnTriggerEnter (Collider hit)
	{
		if (hit.tag == "Grab" && !dragging) 
		{
			grabbedObject = hit.gameObject;
			offset = hit.transform.position - transform.position;
			grabbedExtents = hit.transform.GetComponent<Collider>().bounds.extents;
			dragging = true;
		}
	}

	void OnTriggerExit (Collider hit)
	{
//		if (hit.tag == "Grab")
//		{
//			dragging = false;
//		}
	}

	public GameObject GetGrabbedObject ()
	{
		return grabbedObject;
	}
}
