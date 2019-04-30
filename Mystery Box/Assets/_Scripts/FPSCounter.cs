using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	// Use this for initialization

	public Text tex;
	
	// Update is called once per frame
	void Update () 
	{
		tex.text = (Time.frameCount / Time.time).ToString();
	}
}
