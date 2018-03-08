using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public Transform target;
	public Transform match;
	Vector3 originalPos;
	public float factor;

	void Start() {
		originalPos = transform.position;
	}
	
	void Update () {
		transform.rotation = match.rotation;
		transform.position = originalPos - (match.position * factor);
		// transform.LookAt(target);
	}
}
