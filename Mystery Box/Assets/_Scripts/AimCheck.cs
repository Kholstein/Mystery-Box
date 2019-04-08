using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCheck : MonoBehaviour {


	public bool Blocking;

	void OnCollisionStay(Collision other)
	{
		Blocking = true;
	}
	void OnCollisionExit(Collision other)
	{
		Blocking = false;
	}
}
