using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_script : MonoBehaviour {

	Vector3 offset;
	int speed = 2;
	float x=0;
	float y=0;

	// Use this for initialization
	void Start () {
		createBody (50, 0); // Needs to be half the size of the ground's dimensions.
		createBody (-50, 0);
		createBody (0, 50);
		createBody (0, -50);

	}
	
	// Update is called once per frame
	void Update () {
		// MOVE:
		if (Input.GetKey ("w")) {
			offset.x = Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			offset.z = Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			transform.position += offset;
			//			Debug.Log (transform.rotation.eulerAngles);
		}
		if (Input.GetKey ("s")) {
			offset.x = - Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			offset.z = - Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			transform.position += offset;
		}
		if (Input.GetKey ("a")) {
			offset.x = - Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			offset.z = Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			transform.position += offset;
		}
		if (Input.GetKey ("d")) {
			offset.x =  Mathf.Cos (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			offset.z = - Mathf.Sin (transform.rotation.eulerAngles.y * Mathf.Deg2Rad * speed);
			transform.position += offset;
		}

		// ROTATE (i and k are weird now that we are rotating player instead of camera):
		if (Input.GetKey ("i")) {		
			x -= Time.deltaTime * 50;
		}
		if (Input.GetKey ("k")) {
			x += Time.deltaTime * 50;
		}
		if (Input.GetKey ("j")) {
			y -= Time.deltaTime * 50;
		}
		if (Input.GetKey ("l")) {
			y += Time.deltaTime * 50;
		}

		transform.rotation = Quaternion.Euler(x,y,0);
	}

	// (x, z) is either (100, 0), (-100, 0), (0, 100) or (0, -100):
	void createBody (int x, int z) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Rigidbody rb = wall.AddComponent<Rigidbody>();
		rb.transform.position = new Vector3 (x, 0, z);
		rb.transform.localScale = new Vector3 (2 * Mathf.Abs(z), 10, 2 * Mathf.Abs(x));
		rb.useGravity = false;
	}
}
