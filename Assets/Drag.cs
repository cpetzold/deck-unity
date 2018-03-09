using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	public float speed = 1;
	public float rotateForce = 1;
	Vector3 dest;

	void OnMouseDrag() {
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		dest = new Vector3(mousePos.x, transform.position.y, mousePos.z);
	}

	void Update() {
		transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * speed);

		Vector3 d = transform.position - dest;
     	transform.localRotation = Quaternion.Euler(d.z * rotateForce, 0, d.x * rotateForce);
	}

}
